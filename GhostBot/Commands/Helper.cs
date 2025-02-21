using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
namespace GhostBot.Commands;

public class Helper : BaseCommandModule
{
    [Command("test")]
    public async Task TestCommand(CommandContext ctx)
    {
        await ctx.Channel.SendMessageAsync($"Hello {ctx.User.Username}!");
    }

    [Command("add")]
    public async Task AddCommand(CommandContext ctx, int number1, int number2)
    {
        int result = number1 + number2;
        await ctx.Channel.SendMessageAsync(result.ToString());
    }
    
    [Command("subtract")]
    public async Task SubtractCommand(CommandContext ctx, int number1, int number2)
    {
        int result = number1 - number2;
        await ctx.Channel.SendMessageAsync(result.ToString());
    }
}