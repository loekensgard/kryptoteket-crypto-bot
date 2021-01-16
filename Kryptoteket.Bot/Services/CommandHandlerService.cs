﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Kryptoteket.Bot.Configurations;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Kryptoteket.Bot.Services
{
    public class CommandHandlerService
    {
        private readonly DiscordSocketClient _discordSocketClient;
        private readonly CommandService _commandService;
        private readonly IServiceProvider _services;
        private readonly DiscordConfiguration _discordOptions;
        private const string _messageErrorTemplate = "Discord Error {reasonType} {reasonDescription} {message}";

        public CommandHandlerService(DiscordSocketClient discordSocketClient, CommandService commandService, IServiceProvider services, IOptions<DiscordConfiguration> discordOptions)
        {
            _discordSocketClient = discordSocketClient;
            _commandService = commandService;
            _services = services;
            _discordOptions = discordOptions.Value;

            _discordSocketClient.MessageReceived += OnMessageReceivedAsync;
            _discordSocketClient.ReactionAdded += OnMessageReactionAdd;
            _discordSocketClient.Ready += ReadyAsync;
        }

        private Task OnMessageReactionAdd(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (!message.HasValue) return null;
            if (message.HasValue && message.Value.Source != MessageSource.Bot) return null;



            return null;
        }

        private Task ReadyAsync()
        {
            Log.Information("{user} is connected!", _discordSocketClient.CurrentUser);
            return Task.CompletedTask;
        }

        private async Task OnMessageReceivedAsync(SocketMessage parameterMessage)
        {
            // Don't handle the command if it is a system message
            var message = parameterMessage as SocketUserMessage;
            if (message == null) return;

            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;

            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(_discordSocketClient, message);

            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!(message.HasCharPrefix(_discordOptions.Prefix, ref argPos) ||
                message.HasMentionPrefix(_discordSocketClient.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
            {
                return;
            }

            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            try
            {
                var result = await _commandService.ExecuteAsync(
                    context: context,
                    argPos: argPos,
                    services: _services);

                if (!result.IsSuccess)
                    Log.Error(_messageErrorTemplate, result.Error, result.ErrorReason, message);

            }
            catch (Exception e)
            {
                Log.Error(e, "Failed Excetuing command async");
            }
        }
    }

}
