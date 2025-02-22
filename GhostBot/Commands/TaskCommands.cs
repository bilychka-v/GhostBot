using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace GhostBot.Commands
{
    public class TaskCommands : BaseCommandModule
    {
        private static readonly string filePath = "tasks.json";
        private static List<string> tasks = new List<string>();

        public TaskCommands()
        {
            LoadTasks();
        }

        private static void SaveTasks()
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(tasks));
        }

        private static void LoadTasks()
        {
            if (File.Exists(filePath))
            {
                tasks = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(filePath)) ?? new List<string>();
            }
        }

        [Command("addtask")]
        public async Task AddTask(CommandContext ctx, [RemainingText] string task)
        {
            if (string.IsNullOrWhiteSpace(task))
            {
                await ctx.Channel.SendMessageAsync("❌ Ви не ввели завдання!");
                return;
            }

            tasks.Add(task);
            SaveTasks(); // Збереження у файл
            await ctx.Channel.SendMessageAsync($"✅ Завдання додано: **{task}**");
        }

        [Command("listtasks")]
        public async Task ListTasks(CommandContext ctx)
        {
            if (tasks.Count == 0)
            {
                await ctx.Channel.SendMessageAsync("📜 Список завдань порожній.");
                return;
            }

            string taskList = string.Join("\n", tasks.Select((t, i) => $"{i + 1}. {t}"));
            await ctx.Channel.SendMessageAsync($"📌 **Список завдань:**\n{taskList}");
        }

        [Command("removetask")]
        public async Task RemoveTask(CommandContext ctx, int taskNumber)
        {
            if (taskNumber < 1 || taskNumber > tasks.Count)
            {
                await ctx.Channel.SendMessageAsync("❌ Невірний номер завдання!");
                return;
            }

            string removedTask = tasks[taskNumber - 1];
            tasks.RemoveAt(taskNumber - 1);
            SaveTasks(); // Збереження у файл
            await ctx.Channel.SendMessageAsync($"🗑️ Завдання видалено: **{removedTask}**");
        }

        [Command("donetask")]
        public async Task DoneTask(CommandContext ctx, int taskNumber)
        {
            if (taskNumber < 1 || taskNumber > tasks.Count)
            {
                await ctx.Channel.SendMessageAsync("❌ Невірний номер завдання!");
                return;
            }

            string completedTask = tasks[taskNumber - 1];
            tasks[taskNumber - 1] = $"~~{completedTask}~~ ✅";
            SaveTasks(); // Збереження у файл
            await ctx.Channel.SendMessageAsync($"🎉 Завдання виконано: **{completedTask}**");
        }
    }
}
