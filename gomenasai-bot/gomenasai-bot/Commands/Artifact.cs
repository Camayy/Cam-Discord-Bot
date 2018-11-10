using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using Discord;
using System.Collections;

namespace gomenasai_bot.Commands
{
    public class Name
    {
        public string english { get; set; }
    }

    public class SetInfo
    {
        public int set_id { get; set; }
        public int pack_item_def { get; set; }
        public Name name { get; set; }
    }

    public class CardName
    {
        public string english { get; set; }
    }

    public class CardText
    {
        public string english { get; set; }
    }

    public class MiniImage
    {
        public string @default { get; set; }
    }

    public class LargeImage
    {
        public string @default { get; set; }
    }

    public class IngameImage
    {
        public string @default { get; set; }
    }

    public class CardList
    {
        public int card_id { get; set; }
        public int base_card_id { get; set; }
        public string card_type { get; set; }
        public CardName card_name { get; set; }
        public CardText card_text { get; set; }
        public MiniImage mini_image { get; set; }
        public LargeImage large_image { get; set; }
        public IngameImage ingame_image { get; set; }
        public int hit_points { get; set; }
        public List<object> references { get; set; }
        public string illustrator { get; set; }
        public int? mana_cost { get; set; }
        public int? attack { get; set; }
        public bool? is_black { get; set; }
        public string sub_type { get; set; }
        public int? gold_cost { get; set; }
        public bool? is_green { get; set; }
        public bool? is_red { get; set; }
        public int? armor { get; set; }
        public bool? is_blue { get; set; }
    }

    [JsonObject]
    public class CardSet : IEnumerable<CardList>
    {
        public int version { get; set; }
        public SetInfo set_info { get; set; }
        public List<CardList> card_list { get; set; }

        public IEnumerator<CardList> GetEnumerator()
        {
            return card_list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return card_list.GetEnumerator();
        }
    }

    [JsonObject]
    public class ArtifactRoot : IEnumerable<CardSet>
    {
        public CardSet card_set { get; set; }

        public IEnumerator<CardSet> GetEnumerator()
        {
            yield return (CardSet)card_set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return card_set.GetEnumerator();
        }
    }

    public class Artifact : ModuleBase<SocketCommandContext>
    {
        private static string _jsonString = "ArtifactJson.json";
        public ArtifactRoot _artifactRoot = null;

        [Command("artifact"), Summary("shit")]
        public async Task GetArtifact([Remainder]string card)
        {
            //https://steamcdn-a.akamaihd.net/apps/583950/resource\/card_set_1.0E871AFDD63D1CBD0FB52D924DF1923C4A6D443A.json
            //string address = "https://steamcdn-a.akamaihd.net/apps/583950/resource/card_set_0.BB8732855C64ACE2696DCF5E25DEDD98D134DD2A.json";
            
            _artifactRoot = DeserializeJson();
            EmbedBuilder embed = EmbedCards(card);


            if (embed.Title != null)
            {
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else
            {
                await Context.Channel.SendMessageAsync(Context.Message.Author.Mention + " Card doesn't exist");
            }

        }

        private EmbedBuilder EmbedCards(string cardName)
        {
            string card = cardName.ToLower();
            EmbedBuilder embed = new EmbedBuilder();

            foreach (CardList set in _artifactRoot.card_set)
            {
                string setCard = set.card_name.english.ToLower(); //ToString().ToLower();

                if (card.Contains(setCard))
                {
                    embed.Title = "Card: " + set.card_name.english;
                    embed.AddInlineField("Type: ", set.card_type);
                    embed.WithColor(Color.Blue);
                    embed.WithImageUrl(set.large_image.@default);
                    embed.AddInlineField("Hit points: ", set.hit_points);
                    if (set.armor.HasValue)
                    {
                        embed.AddInlineField("Armour: ", set.armor);
                    }
                    if (set.attack.HasValue)
                    {
                        embed.AddInlineField("Attack: ", set.attack);
                    }
                    if (set.mana_cost.HasValue)
                    {
                        embed.AddInlineField("Mana cost", set.mana_cost);
                    }

                    if (set.is_blue != null)
                    {
                        embed.AddInlineField("Colour", "Blue");
                    }
                    else if (set.is_black != null)
                    {
                        embed.AddInlineField("Colour", "Black");
                    }
                    else if (set.is_green != null)
                    {
                        embed.AddInlineField("Colour", "Green");
                    }
                    else
                    {
                        embed.AddInlineField("Colour", "Red");
                    }
                    break;
                }
            }
            return embed;
        }

        private bool GetOrCreateJson()
        {
            if (!File.Exists(_jsonString))
            {
                throw new NotImplementedException();
            }
            _artifactRoot = DeserializeJson();
            return true;
        }

        private ArtifactRoot DeserializeJson()
        {
            ArtifactRoot set = null;
            try
            {
                set = JsonConvert.DeserializeObject<ArtifactRoot>(File.ReadAllText(_jsonString));
                return set;
            }
            catch (Exception e)
            {
                Console.WriteLine("JSON ERROR: " + e);
                return set;
            }
        }

        [Command("artifactheroes"), Summary("output all cards")]
        public async Task GetArtifactList()
        {
            if (_artifactRoot == null)
            {
                _artifactRoot = DeserializeJson();
            }
            List<object> temp = null;
            foreach (CardList set in _artifactRoot.card_set)
            {
                temp = set.references;
                if (set.card_type == "Hero")
                {
                    await Context.Channel.SendMessageAsync(set.card_name.english);
                }
            }

            Console.WriteLine(temp);
        }
    }
    
}
