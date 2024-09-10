using Steam_Desktop_Authenticator.Exceptions;
using SteamAuth;
using SteamKit2.Authentication;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steam_Desktop_Authenticator
{
    internal class UserFormAuthenticator : IAuthenticator
    {
        private SteamGuardAccount account;
        private int deviceCodesGenerated = 0;

        public UserFormAuthenticator(SteamGuardAccount account)
        {
            this.account = account;
        }

        public Task<bool> AcceptDeviceConfirmationAsync()
        {
            return Task.FromResult(false);
        }

        public async Task<string> GetDeviceCodeAsync(bool previousCodeWasIncorrect)
        {
            // If a code fails wait 30 seconds for a new one to regenerate
            if (previousCodeWasIncorrect)
            {
                // After 2 tries tell the user that there seems to be an issue
                if (deviceCodesGenerated > 2)
                    MessageBox.Show("There seems to be an issue logging into your account with these two factor codes. Are you sure SDA is still your authenticator?");

                await Task.Delay(30000);
            }

            string deviceCode;

            if (account == null)
            {
                //MessageBox.Show("This account already has an authenticator linked. You must remove that authenticator to add SDA as your authenticator.", "Steam Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                var moveAuthenticator = MessageBox.Show($"你的帐号已绑定Steam手机令牌，是否要将令牌移动到此设备？" +
                     $"{System.Environment.NewLine}" +
                     $"移动令牌后你在48小时内产生的交易报价将被Steam暂挂" +
                     $"{System.Environment.NewLine}" +
                     $"但是你可以正常进行报价交易", "Steam Login", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (moveAuthenticator == DialogResult.Yes)
                {
                    throw new LoginException() { MoveAuthenticator = true };
                }

                return null;
            }
            else
            {
                deviceCode = await account.GenerateSteamGuardCodeAsync();
                deviceCodesGenerated++;
            }

            return deviceCode;
        }

        public Task<string> GetEmailCodeAsync(string email, bool previousCodeWasIncorrect)
        {
            string message = "Enter the code sent to your email:";
            if (previousCodeWasIncorrect)
            {
                message = "The code you provided was invalid. Enter the code sent to your email:";
            }

            InputForm emailForm = new InputForm(message);
            emailForm.ShowDialog();
            return Task.FromResult(emailForm.txtBox.Text);
        }
    }
}
