﻿using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Discord;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using MMBot.Blazor.Data;

namespace MMBot.Blazor.Services
{
    public class AccountService : IAccountService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly StateContainer _stateContainer;

        public AccountService(AuthenticationStateProvider authenticationStateProvider, IDCUser user, StateContainer stateContainer)
        {
            _authenticationStateProvider = authenticationStateProvider;
            LoggedUser = user;

            authenticationStateProvider.AuthenticationStateChanged += AuthenticationStateProvider_AuthenticationStateChanged;
            _stateContainer = stateContainer;
            _ = SetLoggedUserAsync();
        }

        private async void AuthenticationStateProvider_AuthenticationStateChanged(Task<AuthenticationState> task) => await SetLoggedUserAsync();

        public IDCUser LoggedUser { get; set; }
        private async Task SetLoggedUserAsync()
        {
            try
            {
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                if (user.Identity.IsAuthenticated)
                {
                    LoggedUser.Name = user.Identity.Name;
                    LoggedUser.Id = ulong.Parse(user.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? "0");
                    LoggedUser.AvatarUrl = user.Claims.FirstOrDefault(c => c.Type == "urn:discord:avatar:url")?.Value;
                    var ids =  user.Claims.FirstOrDefault(c => c.Type == "guilds")?.Value;

                    var o = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                        IgnoreReadOnlyProperties = true,
                    };

                    var channel = JsonSerializer.Deserialize<IList<DCChannel>>(ids, o);

                    LoggedUser.Guilds = channel.Where(x => x.owner ||
                                                       x.PermissionFlags.HasFlag(GuildPermission.Administrator) ||  
                                                       x.PermissionFlags.HasFlag(GuildPermission.ManageChannels) || 
                                                       x.PermissionFlags.HasFlag(GuildPermission.ManageGuild) ||
                                                       x.PermissionFlags.HasFlag(GuildPermission.ManageRoles)).ToList();

                    _stateContainer.SelectedGuildId = LoggedUser.Guilds.FirstOrDefault().id;
                }
            }
            catch
            {

            }
        }
    }
}
