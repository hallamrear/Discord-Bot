using Discord;
using Discord.API.Client;
using Discord.Commands;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscordBotV2
{
    class MyBot
    {

        DiscordClient discord;
        CommandService commands;

        Random rand;

        string[] botMemes;
        string[] randomTexts;

        public MyBot()
        {

            NotifyIcon tray = new NotifyIcon();
            tray.Icon = new Icon(SystemIcons.Application, 40, 40);
            tray.Visible = true;
            

            botMemes = new string[]
            {
                "memes/meme1.jpg",
                "memes/meme2.jpg",
                "memes/meme3.png"
            };

            randomTexts = new string[]
            {
                "It doesn't matter. Listen kid you don't want to see my other side. I have a wolf inside me with a muzzle on, but the muzzle is about to come off. You broke her heart, and I will break yours. She is a nice girl, how dare you use her like this. How come people like you get to date her? Then people like me have to sit in the shadows and be the shoulders to cry on. Listen Kid, I don't have time for FUCKING games. I am a nice guy, but when you make a nice guy angry; the world shakes. Don't do it again.\n \n... You will regret this the next FULL MOON. You mess with me you mess with the pack bud. FUCK you. Get ready.",
                "When I was young my father said to me: \"Knowledge is Power....Francis Bacon\" I understood it as \"Knowledge is power, France is Bacon\". For more than a decade I wondered over the meaning of the second part and what was the surreal linkage between the two? If I said the quote to someone, \"Knowledge is power, France is Bacon\" they nodded knowingly. Or someone might say, \"Knowledge is power\" and I'd finish the quote \"France is Bacon\" and they wouldn't look at me like I'd said something very odd but thoughtfully agree. I did ask a teacher what did \"Knowledge is power, France is bacon\" mean and got a full 10 minute explanation of the Knowledge is power bit but nothing on \"France is bacon\". When I prompted further explanation by saying \"France is Bacon?\" in a questioning tone I just got a \"yes\". at 12 I didn't have the confidence to press it further. I just accepted it as something I'd never understand. It wasn't until years later I saw it written down that the penny dropped.",
                "gr8 m8 no d-b8 i r8 it an 8 i h8 2 b in an ir8 st8 but its my f8 hey m8 i apreci8 that u r8 it gr8 u wanna d8 and mayb masturb8 i can ask n8 and we can meet at the g8 dont b l8 gr8 b8 m8 i r8 it an 8/8 plz don't h8 gr8 b8 m8 cant even h8 so I r8 8 outta 8 Gr8 b8 m8. I rel8, str8 appreci8, and congratul8. I r8 this b8 an 8/8. Plz no h8., I'm str8 ir8. Cre8 more, can't w8. We should convers8, I won't ber8.",
                "Sodium, atomic number 11, was first isolated by Humphry Davy in 1807. A chemical component of salt, he named it Na in honor of the saltiest region on earth, North America.",
                "I sexually Identify as an the sun. Ever since I was a boy I dreamed of slamming hydrogen isotopes into each other to make helium & light and send it throught the galaxy. People say to me that a person being a star is Impossible and I’m fucking retarded but I don’t care, I’m beautiful. I’m having a plastic surgeon inflate me with hydrogen and raise my temperature to over 6000 °C. From now on I want you guys to call me “Sol” and respect my right to give you vitamin D and probably sunburns. If you can’t accept me you’re a fusionphobe and need to check your astral privilege. Thank you for being so understanding.",
                "High in orbit, the Gitraktmaet motherships descend upon the Earth. They prepare to enslave the world and mine it for all its salt, but the scanners detect an abnormally high concentration inside a tiny shack in Greece. The invasion won't be necessary. \"Lock onto him with the RNG disruptor,\" says the captain, greedily. \"Soon we shall have all the salt we need.\"",
                " ﻿ＨＥＬＬＯ ＡＭ ４８ ＹＥＡＲ ＭＡＮ ＦＲＯＭ ＳＯＭＡＬＩＡ． ＳＯＲＲＹ ＦＯＲ ＢＡＤ ＥＮＧＬＡＮＤ． Ｉ ＳＥＬＬＥＤ ＭＹ ＷＩＦＥ ＦＯＲ ＩＮＴＥＲＮＥＴ ＣＯＮＮＥＣＴＩＯＮ ＦＯＲ ＰＬＡＹ ＂ｈｅａｒｔｈ ｓｔｏｎｅ＂ ＡＮＤ Ｉ ＷＡＮＴ ＴＯ ＢＥＣＯＭＥ ＴＨＥ ＧＯＯＤＥＳＴ ＰＬＡＹＥＲ ＬＩＫＥ ＹＯＵ Ｉ ＰＬＡＹ ＷＩＴＨ ４００ ＰＩＮＧ ＯＮ ＢＲＡＺＩＬ ＳＥＲＶＥＲ ＡＮＤ Ｉ ＡＭ ＲＡＮＫ ２３ ＡＬＲＥＡＤＹ ＰＬＳ ＮＯ ＣＯＰＹ ＰＡＳＴＥ ＭＹ ＳＴＯＲＹ",

            };

            rand = new Random();

            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            }
            );

            discord.UsingCommands(x => 
            {
                x.PrefixChar = ';';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService <CommandService>();



            discord.UserJoined += async (s, e) =>
            {
                var channel = e.Server.FindChannels("general", ChannelType.Text).FirstOrDefault(); ;

                var user = e.User;

                await channel.SendMessage(string.Format("{0} has joined the server.", user.Name));
            };



            discord.UserLeft += async (s, e) =>
            {
                var channel = e.Server.FindChannels("general", ChannelType.Text).FirstOrDefault();

                var user = e.User;

                await channel.SendMessage(string.Format("{0} has left the server.", user.Name));

            };

            discord.JoinedServer += async (s, e) =>
            {
                var channel = e.Server.FindChannels("general", ChannelType.Text).FirstOrDefault();
                await channel.SendMessage(("Well, I guess i'm here now. Follow my creator on twitter @itssadden. #ShamelessSelfAdvertising"));
            };



            RegisterCommands();




            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzA3OTEyMzI1MTI0NDU2NDUx.C-ZPDA.M2KZuctQ3YXDgk_VaDqNp02mcDo", TokenType.Bot);
            });

        }




        private void RegisterCommands()
        {

            commands.CreateCommand("help")
                .Do(async (e) =>
                {
                await e.Channel.SendMessage(
                    ";help - shows this message \n;hello - hello world! \n;announce - announce things in different channels \n;delete - deletes the last 100 messages \n;meme - another meme from my collection. \n;copypasta - the best things on the internet \n;rekt - damn son \n;music - WIP"
                    );
                });


            commands.CreateCommand("hello")
                .Do(async (e) =>
                {
                    string user;
                    user = e.User.ToString();
                    await e.Channel.SendMessage("Hi " + user + "!");
                });

            commands.CreateCommand("meme")
                .Do(async (e) =>
                {
                    int memeNumber = rand.Next(botMemes.Length);
                    string memeToPost = botMemes[memeNumber];
                    await e.Channel.SendFile(memeToPost);
                });

            commands.CreateCommand("copypasta")
                .Do(async (e) =>
                {
                    int textNumber = rand.Next(randomTexts.Length);
                    string textToPost = randomTexts[textNumber];
                    await e.Channel.SendMessage(textToPost);
                });

            commands.CreateCommand("delete")
                .Do(async(e) => 
                {
                    Discord.Message[] messagesToDelete;
                    messagesToDelete = await e.Channel.DownloadMessages(100);
                    await e.Channel.DeleteMessages(messagesToDelete);
                    await e.Channel.SendMessage("Deleted last 100 messages in the " + e.Channel.Name + " Channel.");
                });

            commands.CreateCommand("rekt")
                .Do(async(e) => 
                {
                     await e.Channel.SendMessage(":no_entry_sign: Not rekt \n☑ Rekt\n☑ Really Rekt\n☑ Tyrannosaurus Rekt\n☑ Cash4Rekt.com\n☑ Grapes of Rekt\n☑ Ship Rekt\n☑ Rekt markes the spot\n☑ Caught rekt handed\n☑ The Rekt Side Story\n☑ Singin' In The Rekt\n☑ Painting The Roses Rekt\n☑ Rekt Van Winkle\n☑ Parks and Rekt\n☑ Lord of the Rekts: The Reking of the King\n☑ Star Trekt\n☑ The Rekt Prince of Bel - Air\n☑ A Game of Rekt\n☑ Rektflix\n☑ Rekt it like it's hot\n☑ RektBox 360\n☑ The Rekt - men\n☑ School Of Rekt\n☑ I am Fire, I am Rekt\n☑ Rekt and Roll\n☑ Professor Rekt\n☑ Catcher in the Rekt\n☑ Rekt - 22\n☑ Harry Potter: The Half - Rekt Prince\n☑ Great Rektspectations\n☑ Paper Scissors Rekt\n☑ RektCraft\n☑ Grand Rekt Auto V\n☑ Call of Rekt: Modern Reking 2\n☑ Legend Of Zelda: Ocarina of Rekt\n☑ Rekt It Ralph\n☑ Left 4 Rekt\n☑ www.rekkit.com\n☑ Pokemon: Fire Rekt\n☑ The Shawshank Rektemption\n☑ The Rektfather\n☑ The Rekt Knight\n☑ Fiddler on the Rekt\n☑ The Rekt Files\n☑ The Good, the Bad, and The Rekt\n☑ Forrekt Gump\n☑ The Silence of the Rekts\n☑ The Green Rektn\n☑ Gladirekt\n☑ Spirekted Away\n☑ Terminator 2: Rektment Day\n☑ The Rekt Knight Rises\n☑ The Rekt King\n☑ REKT - E\n☑ Citizen Rekt\n☑ Requiem for a Rekt\n☑ REKT TO REKT ass to ass\n☑ Star Wars: Episode VI - Return of the Rekt\n☑ Braverekt\n☑ Batrekt Begins\n☑ 2001: A Rekt Odyssey\n☑ The Wolf of Rekt Street\n☑ Rekt's Labyrinth\n☑ 12 Years a Rekt\n☑ Gravirekt\n☑ Finding Rekt\n☑ The Arekters\n☑ There Will Be Rekt\n☑ Christopher Rektellston\n☑ Hachi: A Rekt Tale\n☑ The Rekt Ultimatum\n☑ Shrekt\n☑ Rektal Exam\n☑ Rektium for a Dream\n☑ www.Trekt.tv\n☑ Erektile Dysfunction");
                });

            commands.CreateCommand("setup")
                .Do(async (e) =>
                {
                    if (e.Message.User.ToString() == "sadden#0125")
                    {
                        await e.Channel.SendMessage("asuhdude");
                    }
                    else
                    {
                        await e.Channel.SendMessage("who da hell r u");
                    }
                });

            commands.CreateCommand("announce").Parameter("message", ParameterType.Multiple).Do(async (e) =>
            {
                await DoAnnouncement(e);
            }
            );
        }

        private async Task DoAnnouncement(CommandEventArgs e)
        {
            var channel = e.Server.FindChannels(e.Args[0], ChannelType.Text).FirstOrDefault();

            var message = ConstructMessage(e, channel != null);

            if (channel != null)
            {
                await channel.SendMessage(message);
            }
            else
            {
                await e.Channel.SendMessage(message);
            }

        }

        private string ConstructMessage(CommandEventArgs e, bool firstArgisChannel)
        {
            string message = "";
            var name = e.User.Nickname != null ? e.User.Nickname : e.User.Name;

            var startIndex = firstArgisChannel ? 1 : 0;

            for (int i = startIndex; i < e.Args.Length; i++)
            {
                message += e.Args[i].ToString() + " ";
            }

            var result = name + " says: " + message;

            return result;

        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
