using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamAuth;
using SteamKit2.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Steam_Desktop_Authenticator
{
    internal class ServerListProvider : IServerListProvider
    {
        private IEnumerable<ServerRecord> _servers = Enumerable.Empty<ServerRecord>();

        /// <summary>
        /// Returns the stored server list in memory
        /// </summary>
        /// <returns>List of servers if persisted, otherwise an empty list</returns>
        public async Task<IEnumerable<ServerRecord>> FetchServerListAsync()
        {
            if (_servers.Any())
            {
                return _servers;
            }

            using (HttpClient client = new HttpClient(new HttpClientHandler()))
            {
                var response = await client.GetAsync(new Uri($"{APIEndpoints.STEAMAPI_BASE}/ISteamDirectory/GetCMList/v1/?cellid=0&count=100"));
                string body = await response.Content.ReadAsStringAsync();
                var query = JsonConvert.DeserializeObject<JObject>(body)["response"];
                if (query == null)
                {
                    return _servers;
                }

                var socketList = query["serverlist"];
                var websocketList = query["serverlist_websockets"];

                var serverRecords = new List<ServerRecord>();
                if (socketList != null)
                {
                    foreach (var child in socketList)
                    {
                        if (!ServerRecord.TryCreateSocketServer(child.Value<string>(), out var record))
                        {
                            continue;
                        }

                        serverRecords.Add(record);
                    }
                }

                if (websocketList != null)
                {
                    foreach (var child in websocketList)
                    {

                        serverRecords.Add(ServerRecord.CreateWebSocketServer(child.Value<string>()));
                    }
                }

                return serverRecords.AsReadOnly();
            }
        }

        /// <summary>
        /// Stores the supplied list of servers in memory
        /// </summary>
        /// <param name="endpoints">Server list</param>
        /// <returns>Completed task</returns>
        public Task UpdateServerListAsync(IEnumerable<ServerRecord> endpoints)
        {
            _servers = endpoints;
            return Task.CompletedTask;
        }
    }
}
