using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Steam_Desktop_Authenticator.Exceptions;
using SteamAuth;
using SteamKit2;
using SteamKit2.Authentication;
using SteamKit2.Internal;

namespace Steam_Desktop_Authenticator
{
    public partial class LoginForm : Form
    {
        public SteamGuardAccount account;
        public LoginType LoginReason;
        public SessionData Session;

        public LoginForm(LoginType loginReason = LoginType.Initial, SteamGuardAccount account = null)
        {
            InitializeComponent();
            this.LoginReason = loginReason;
            this.account = account;

            try
            {
                if (this.LoginReason != LoginType.Initial)
                {
                    txtUsername.Text = account.AccountName;
                    txtUsername.Enabled = false;
                }

                if (this.LoginReason == LoginType.Refresh)
                {
                    labelLoginExplanation.Text = "Your Steam credentials have expired. For trade and market confirmations to work properly, please login again.";
                }
                else if (this.LoginReason == LoginType.Import)
                {
                    labelLoginExplanation.Text = "Please login to your Steam account import it.";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to find your account. Try closing and re-opening SDA.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        public void SetUsername(string username)
        {
            txtUsername.Text = username;
        }

        public string FilterPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Replace("-", "").Replace("(", "").Replace(")", "");
        }

        public bool PhoneNumberOkay(string phoneNumber)
        {
            if (phoneNumber == null || phoneNumber.Length == 0) return false;
            if (phoneNumber[0] != '+') return false;
            return true;
        }

        private void ResetLoginButton()
        {
            btnSteamLogin.Enabled = true;
            btnSteamLogin.Text = "Login";
        }

        private async void btnSteamLogin_Click(object sender, EventArgs e)
        {
            // Disable button while we login
            btnSteamLogin.Enabled = false;
            btnSteamLogin.Text = "Logging in...";

            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Start a new SteamClient instance
            SteamClient steamClient = new SteamClient(SteamConfiguration.Create(configurator =>
            {
                Manifest man = Manifest.GetManifest();
                configurator.WithWebAPIBaseAddress(new Uri(man.WebApiAddress));
                configurator.WithServerListProvider(new ServerListProvider());
            }));

            // Connect to Steam
            steamClient.Connect();

            // Really basic way to wait until Steam is connected
            while (!steamClient.IsConnected)
                await Task.Delay(500);

            // Create a new auth session
            CredentialsAuthSession authSession;
            try
            {
                authSession = await steamClient.Authentication.BeginAuthSessionViaCredentialsAsync(new AuthSessionDetails
                {
                    Username = username,
                    Password = password,
                    IsPersistentSession = false,
                    PlatformType = EAuthTokenPlatformType.k_EAuthTokenPlatformType_MobileApp,
                    ClientOSType = EOSType.Android9,
                    Authenticator = new UserFormAuthenticator(this.account),
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Steam Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Starting polling Steam for authentication response
            AuthPollResult pollResponse;

            AuthenticatorLinker.LinkResult linkResponse = AuthenticatorLinker.LinkResult.GeneralFailure;

            try
            {
                pollResponse = await authSession.PollingWaitForResultAsync();
            }
            catch (Exception ex) when (ex is LoginException loginException && loginException.MoveAuthenticator)
            {
                InputForm guardCodeForm = new InputForm("������Steam������֤���Ե�¼���Steam�ʺ�");
                while (true)
                {
                    try
                    {
                        guardCodeForm.ShowDialog();
                        if (!guardCodeForm.Canceled)
                        {
                            string guardCode = guardCodeForm.txtBox.Text;

                            await authSession.SendSteamGuardCodeAsync(guardCode, EAuthSessionGuardType.k_EAuthSessionGuardType_DeviceCode);
                            pollResponse = await authSession.PollAuthSessionStatusAsync();
                            linkResponse = AuthenticatorLinker.LinkResult.BeginMoveAuthenticator;
                            break;
                        }
                    }
                    catch (AuthenticationException authenticationException) when (authenticationException.Result == EResult.TwoFactorCodeMismatch)
                    {
                        guardCodeForm = new InputForm("������֤���������������");
                        continue;
                    }
                    catch (Exception ex1)
                    {
                        MessageBox.Show(ex1.Message, "Steam Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Steam Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Build a SessionData object
            SessionData sessionData = new SessionData()
            {
                SteamID = authSession.SteamID.ConvertToUInt64(),
                AccessToken = pollResponse.AccessToken,
                RefreshToken = pollResponse.RefreshToken,
            };

            //Login succeeded
            this.Session = sessionData;

            // If we're only logging in for an account import, stop here
            if (LoginReason == LoginType.Import)
            {
                this.Close();
                return;
            }

            // If we're only logging in for a session refresh then save it and exit
            if (LoginReason == LoginType.Refresh)
            {
                Manifest man = Manifest.GetManifest();
                account.FullyEnrolled = true;
                account.Session = sessionData;
                HandleManifest(man, true);
                this.Close();
                return;
            }

            // Begin linking mobile authenticator
            AuthenticatorLinker linker = new AuthenticatorLinker(sessionData);

            if (linkResponse == AuthenticatorLinker.LinkResult.BeginMoveAuthenticator)
            {
                InputForm smsCodeForm;

                while (linkResponse != AuthenticatorLinker.LinkResult.AwaitingFinalizeMoveAuthenticator)
                {
                    try
                    {
                        linkResponse = await linker.BeginMoveAuthenticatorAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("�ƶ�����ʧ��: " + ex.Message, "�ƶ�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ResetLoginButton();
                        return;
                    }

                    switch (linkResponse)
                    {
                        case AuthenticatorLinker.LinkResult.MustProvidePhoneNumber:
                            MessageBox.Show("���Steam�ʺ�δ���ֻ��ţ��ƶ�����֮ǰ��ҪΪ����ֻ���", "�ƶ�����", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            PhoneInputForm phoneInputForm = new PhoneInputForm(account);
                            phoneInputForm.ShowDialog();
                            if (phoneInputForm.Canceled)
                            {
                                this.Close();
                                return;
                            }

                            linker.PhoneNumber = phoneInputForm.PhoneNumber;
                            linker.PhoneCountryCode = phoneInputForm.CountryCode;
                            break;

                        case AuthenticatorLinker.LinkResult.MustConfirmEmail:
                            MessageBox.Show("���������ȷ�ϣ����ڼ���֮ǰ����Steam���͸��������ӡ�", "�ƶ�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;

                        case AuthenticatorLinker.LinkResult.AwaitingFinalizationAddPhone:
                            smsCodeForm = new InputForm($"������Ϊ���Steam�ʺ�����ֻ���,�������ֻ�������֤��");
                            while (true)
                            {
                                smsCodeForm.ShowDialog();
                                if (!smsCodeForm.Canceled)
                                {
                                    string smsCode = smsCodeForm.txtBox.Text;

                                    var finalizeMoveAuthenticator = await linker.VerifyAccountPhoneWithCodeAsync(smsCode);
                                    if (finalizeMoveAuthenticator == ErrorCodes.OK)
                                    {
                                        break;
                                    }

                                    switch (finalizeMoveAuthenticator)
                                    {
                                        case ErrorCodes.NoMatch:
                                        case ErrorCodes.SMSCodeFailed:
                                            smsCodeForm = new InputForm("��֤���������������");
                                            continue;
                                    }
                                }

                                MessageBox.Show($"����ֻ���ʧ��", "�ƶ�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                                return;
                            }
                            break;

                        case AuthenticatorLinker.LinkResult.AddPhoneError:
                            MessageBox.Show($"����ֻ���ʧ��", "�ƶ�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                            return;

                        case AuthenticatorLinker.LinkResult.SendSmsCodeError:
                            MessageBox.Show($"���Ͷ�����֤��ʧ��" +
                                $"{Environment.NewLine}" +
                                $"��ȷ������ֻ���û�б�Steam����," +
                                $"{Environment.NewLine}" +
                                $"�����㷢����֤��Ĵ���û�г���Steam����", "�ƶ�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                            return;

                        case AuthenticatorLinker.LinkResult.MoveAuthenticatorFail:
                            MessageBox.Show($"�ƶ�����ʧ��", "�ƶ�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                            return;
                    }
                }

                smsCodeForm = new InputForm($"�������ƶ����Steam������֤��,��֤�����ѷ��͵���Steam�ʺŰ󶨵��ֻ�����,�������ֻ�������֤��" +
                    $"{Environment.NewLine}" +
                    $"�ƶ����ƺ�����48Сʱ�ڲ����Ľ��ױ��۽���Steam�ݹ�" +
                    $"{System.Environment.NewLine}" +
                    $"����������������б��۽���");
                while (true)
                {
                    try
                    {
                        smsCodeForm.ShowDialog();
                        if (!smsCodeForm.Canceled)
                        {
                            string smsCode = smsCodeForm.txtBox.Text;

                            var finalizeMoveAuthenticator = await linker.FinalizeMoveAuthenticatorAsync(smsCode);
                            if (finalizeMoveAuthenticator == ErrorCodes.OK)
                            {
                                break;
                            }

                            switch (finalizeMoveAuthenticator)
                            {
                                case ErrorCodes.NoMatch:
                                case ErrorCodes.SMSCodeFailed:
                                    smsCodeForm = new InputForm("��֤���������������");
                                    continue;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "�ƶ�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.Close();
                    return;
                }

                Manifest manifest = Manifest.GetManifest();
                string passKey = null;
                if (manifest.Entries.Count == 0)
                {
                    passKey = manifest.PromptSetupPassKey("Please enter an encryption passkey. Leave blank or hit cancel to not encrypt (VERY INSECURE).");
                }
                else if (manifest.Entries.Count > 0 && manifest.Encrypted)
                {
                    bool passKeyValid = false;
                    while (!passKeyValid)
                    {
                        InputForm passKeyForm = new InputForm("Please enter your current encryption passkey.");
                        passKeyForm.ShowDialog();
                        if (!passKeyForm.Canceled)
                        {
                            passKey = passKeyForm.txtBox.Text;
                            passKeyValid = manifest.VerifyPasskey(passKey);
                            if (!passKeyValid)
                            {
                                MessageBox.Show("That passkey is invalid. Please enter the same passkey you used for your other accounts.");
                            }
                        }
                        else
                        {
                            this.Close();
                            return;
                        }
                    }
                }

                manifest.SaveAccount(linker.LinkedAccount, passKey != null, passKey);
            }
            else
            {
                // Show a dialog to make sure they really want to add their authenticator
                var result = MessageBox.Show("Steam account login succeeded. Press OK to continue adding SDA as your authenticator.", "Steam Login", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Cancel)
                {
                    MessageBox.Show("Adding authenticator aborted.", "Steam Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ResetLoginButton();
                    return;
                }

                while (linkResponse != AuthenticatorLinker.LinkResult.AwaitingFinalization)
                {
                    try
                    {
                        linkResponse = await linker.AddAuthenticator();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding your authenticator: " + ex.Message, "Steam Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ResetLoginButton();
                        return;
                    }

                    switch (linkResponse)
                    {
                        case AuthenticatorLinker.LinkResult.MustProvidePhoneNumber:

                            // Show the phone input form
                            PhoneInputForm phoneInputForm = new PhoneInputForm(account);
                            phoneInputForm.ShowDialog();
                            if (phoneInputForm.Canceled)
                            {
                                this.Close();
                                return;
                            }

                            linker.PhoneNumber = phoneInputForm.PhoneNumber;
                            linker.PhoneCountryCode = phoneInputForm.CountryCode;
                            break;

                        case AuthenticatorLinker.LinkResult.AuthenticatorPresent:
                            MessageBox.Show("This account already has an authenticator linked. You must remove that authenticator to add SDA as your authenticator.", "Steam Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                            return;

                        case AuthenticatorLinker.LinkResult.FailureAddingPhone:
                            MessageBox.Show("Failed to add your phone number. Please try again or use a different phone number.", "Steam Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            linker.PhoneNumber = null;
                            break;

                        case AuthenticatorLinker.LinkResult.MustRemovePhoneNumber:
                            linker.PhoneNumber = null;
                            break;

                        case AuthenticatorLinker.LinkResult.MustConfirmEmail:
                            MessageBox.Show("Please check your email, and click the link Steam sent you before continuing.", "Steam Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;

                        case AuthenticatorLinker.LinkResult.GeneralFailure:
                            MessageBox.Show("Error adding your authenticator.", "Steam Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                            return;
                    }
                } // End while loop checking for AwaitingFinalization

                Manifest manifest = Manifest.GetManifest();
                string passKey = null;
                if (manifest.Entries.Count == 0)
                {
                    passKey = manifest.PromptSetupPassKey("Please enter an encryption passkey. Leave blank or hit cancel to not encrypt (VERY INSECURE).");
                }
                else if (manifest.Entries.Count > 0 && manifest.Encrypted)
                {
                    bool passKeyValid = false;
                    while (!passKeyValid)
                    {
                        InputForm passKeyForm = new InputForm("Please enter your current encryption passkey.");
                        passKeyForm.ShowDialog();
                        if (!passKeyForm.Canceled)
                        {
                            passKey = passKeyForm.txtBox.Text;
                            passKeyValid = manifest.VerifyPasskey(passKey);
                            if (!passKeyValid)
                            {
                                MessageBox.Show("That passkey is invalid. Please enter the same passkey you used for your other accounts.");
                            }
                        }
                        else
                        {
                            this.Close();
                            return;
                        }
                    }
                }

                //Save the file immediately; losing this would be bad.
                if (!manifest.SaveAccount(linker.LinkedAccount, passKey != null, passKey))
                {
                    manifest.RemoveAccount(linker.LinkedAccount);
                    MessageBox.Show("Unable to save mobile authenticator file. The mobile authenticator has not been linked.");
                    this.Close();
                    return;
                }

                MessageBox.Show("The Mobile Authenticator has not yet been linked. Before finalizing the authenticator, please write down your revocation code: " + linker.LinkedAccount.RevocationCode);

                AuthenticatorLinker.FinalizeResult finalizeResponse = AuthenticatorLinker.FinalizeResult.GeneralFailure;
                while (finalizeResponse != AuthenticatorLinker.FinalizeResult.Success)
                {
                    InputForm smsCodeForm = new InputForm("Please input the SMS code sent to your phone.");
                    smsCodeForm.ShowDialog();
                    if (smsCodeForm.Canceled)
                    {
                        manifest.RemoveAccount(linker.LinkedAccount);
                        this.Close();
                        return;
                    }

                    InputForm confirmRevocationCode = new InputForm("Please enter your revocation code to ensure you've saved it.");
                    confirmRevocationCode.ShowDialog();
                    if (confirmRevocationCode.txtBox.Text.ToUpper() != linker.LinkedAccount.RevocationCode)
                    {
                        MessageBox.Show("Revocation code incorrect; the authenticator has not been linked.");
                        manifest.RemoveAccount(linker.LinkedAccount);
                        this.Close();
                        return;
                    }

                    string smsCode = smsCodeForm.txtBox.Text;
                    finalizeResponse = await linker.FinalizeAddAuthenticator(smsCode);

                    switch (finalizeResponse)
                    {
                        case AuthenticatorLinker.FinalizeResult.BadSMSCode:
                            continue;

                        case AuthenticatorLinker.FinalizeResult.UnableToGenerateCorrectCodes:
                            MessageBox.Show("Unable to generate the proper codes to finalize this authenticator. The authenticator should not have been linked. In the off-chance it was, please write down your revocation code, as this is the last chance to see it: " + linker.LinkedAccount.RevocationCode);
                            manifest.RemoveAccount(linker.LinkedAccount);
                            this.Close();
                            return;

                        case AuthenticatorLinker.FinalizeResult.GeneralFailure:
                            MessageBox.Show("Unable to finalize this authenticator. The authenticator should not have been linked. In the off-chance it was, please write down your revocation code, as this is the last chance to see it: " + linker.LinkedAccount.RevocationCode);
                            manifest.RemoveAccount(linker.LinkedAccount);
                            this.Close();
                            return;
                    }
                }

                //Linked, finally. Re-save with FullyEnrolled property.
                manifest.SaveAccount(linker.LinkedAccount, passKey != null, passKey);
            }

            MessageBox.Show("Mobile authenticator successfully linked. Please write down your revocation code: " + linker.LinkedAccount.RevocationCode);
            this.Close();
        }

        private void HandleManifest(Manifest man, bool IsRefreshing = false)
        {
            string passKey = null;
            if (man.Entries.Count == 0)
            {
                passKey = man.PromptSetupPassKey("Please enter an encryption passkey. Leave blank or hit cancel to not encrypt (VERY INSECURE).");
            }
            else if (man.Entries.Count > 0 && man.Encrypted)
            {
                bool passKeyValid = false;
                while (!passKeyValid)
                {
                    InputForm passKeyForm = new InputForm("Please enter your current encryption passkey.");
                    passKeyForm.ShowDialog();
                    if (!passKeyForm.Canceled)
                    {
                        passKey = passKeyForm.txtBox.Text;
                        passKeyValid = man.VerifyPasskey(passKey);
                        if (!passKeyValid)
                        {
                            MessageBox.Show("That passkey is invalid. Please enter the same passkey you used for your other accounts.", "Steam Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        this.Close();
                        return;
                    }
                }
            }

            man.SaveAccount(account, passKey != null, passKey);
            if (IsRefreshing)
            {
                MessageBox.Show("Your session was refreshed.", "Steam Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Mobile authenticator successfully linked. Please write down your revocation code: " + account.RevocationCode, "Steam Login", MessageBoxButtons.OK);
            }
            this.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (account != null && account.AccountName != null)
            {
                txtUsername.Text = account.AccountName;
            }
        }

        public enum LoginType
        {
            Initial,
            Refresh,
            Import
        }
    }
}
