﻿namespace MMBot.Blazor.Server.Helpers;

public static class DiscordHelpers
{
    public static bool IsOwner(this DCUser sgu) => sgu.Id == 301764235887902727;
}
