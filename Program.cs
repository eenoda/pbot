using Discord;
using Discord.WebSocket;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        List<int> point = new List<int>();
        List<string> UserID = new List<string>();
        List<bool> isDc = new List<bool>();
        private readonly DiscordSocketClient _client;
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public Program()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;
            _client.Ready += Ready;
            _client.MessageReceived += MessageReceivedAsync;
        }

        public async Task MainAsync()
        {
            await _client.LoginAsync(TokenType.Bot, "Njk1OTEwOTEyMzM4NjkwMDc4.XohhLA.MEmeSvLbuCThvmahh9UGCJxpxcU");
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private Task Ready()
        {
            Console.WriteLine($"{_client.CurrentUser} 연결됨!");
            point = new List<int>();
            UserID = new List<string>();
            isDc = new List<bool>();
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(SocketMessage message)
        {
            if (message.Author.Id == _client.CurrentUser.Id)
                return;
            if (message.Content == "pp dcdc")
            {
                if (message.Author.Username == "이노다" || message.Author.Username == "푸여")
                {
                    for (int i = 0; i < isDc.Count; i++)
                    {
                        isDc[i] = true;
                    }
                }
            }
            if (message.Content == "pp dc")
            {
                bool isFound = false;
                for (int i = 0; i < isDc.Count; i++)
                {
                    if (UserID[i] == message.Author.Id.ToString())
                    {
                        if (!isDc[i])
                        {
                            await message.Channel.SendMessageAsync("```fix" + "\n" + "우리는 UserId 리스트에서 당신의 고유 아이디를 찾았지만 당신의 isDc의 값은 이미 false입니다. 내일 또 와주세요" + "```");
                        }
                        else
                        {
                            await message.Channel.SendMessageAsync("```fix" + "\n" + message.Author.Username + "님! 축하드려요! 당신은 출석 체크 보상으로 40포인트를 얻었습니다" + "```");
                            point[i] += 40;
                            isFound = true;
                            isDc[i] = false;
                            break;
                        }
                    }
                    else
                    {
                        isFound = false;
                    }
                }
                if (isFound == false)
                {
                    UserID.Add(message.Author.Id.ToString());
                    point.Add(0);
                    isDc.Add(true);
                    await message.Channel.SendMessageAsync("```fix" + "\n" + "우리는 UserId 리스트에서 당신의 고유 아이디를 찾지 못 했습니다. 그렇기 때문에 당신의 식별 ID를 세로 만들었고 다시 한번 이 명령어를 실행하면 올바르게 작동할 것입니다.[그럼에도 불구하고 작동을 하지 않는다면 죄송합니다. 결함이 있나 봐요]" + "```");
                }
            }
            if (message.Content == "pp pc")
            {
                bool isFound = false;
                if (UserID != null)
                {
                    for (int i = 0; i < UserID.Count; i++)
                    {
                        if (UserID[i] == message.Author.Id.ToString())
                        {
                            await message.Channel.SendMessageAsync("```fix" + "\n" + message.Author.Username + "님의 포인트는 " + point[i] + "입니다!" + "```");
                            isFound = true;
                            break;
                        }
                        else
                        {
                            isFound = false;
                        }
                    }
                    if (isFound == false)
                    {
                        UserID.Add(message.Author.Id.ToString());
                        point.Add(0);
                    }
                }
                else
                {
                    UserID.Add(message.Author.Id.ToString());
                    point.Add(0);
                    isDc.Add(true);
                    await message.Channel.SendMessageAsync("```fix" + "\n" + message.Author.Username + "님의 포인트는 " + 0 + "입니다!" + "```");
                }
            }
        }

    }
}
