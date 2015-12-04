using IdeaManMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdeaManMVC.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IdeaManMVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IdeaManMVC.Models.ApplicationDbContext context)
        {
            using (UserManager<ApplicationUser> manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context)))
            {
                if (manager.FindByEmail("admin@agile.com") != null
                &&  manager.IsInRole(manager.FindByEmail("admin@agile.com").Id, "Moderator")
                ) return;
            }
            context.Votes.RemoveRange(context.Votes);
            context.Ideas.RemoveRange(context.Ideas);
            context.AppUsers.RemoveRange(context.AppUsers);
            FillUsers(context);
            FillIdeas(context);
        }

        private void FillIdeas(ApplicationDbContext context)
        {
            
            var ideaTitles = new string[]
            {
                "Adversely this hen more.",
                "Crud darn much grimaced.",
                "That prissily chose oh.",
                "Some up less alongside.",
                "Alas sheep extravagantly forward.",
                "Much jeez dear crud.",
                "A dwelled gosh caterpillar.",
                "A well this sheepishly.",
                "Beaver much and peculiar.",
                "Neatly without ouch hurried.",
                "Far hence and this.",
                "And redid more under.",
                "On less far hey.",
                "Lethargic jeez stoically excluding.",
                "Hedgehog since chose less.",
                "This abrasively egret fox.",
                "Turned that that vulgarly.",
                "One and curious lamely.",
                "And chose some criminal.",
                "On dear a well."
            };
            var ideasList = new string[]
            {
                "Crud jeepers withdrew during stupidly alas oh austerely dolphin messily walrus so shot gecko when.",
                "Bled differently tore and gnu heinously darn wasp oh more that well spelled tamarin other naked considering far alleged some circa since.",
                "Dragonfly some impala much however reindeer a leopard jeepers since hello beneath goodness or much.",
                "Animated shark cockatoo regardless dear rigorous alas impious jeepers scorpion cut bald one mammoth overcast far split magnanimously hey dear much chortled.",
                "And oafishly oh darn by jeering bawdily selfish and impetuous towards impiously coaxingly radiantly this.",
                "Gosh mechanically cut cagily after funny far giggled more reset noisy from yikes one that darn komodo wedded the cracked fraternal dear jellyfish laughing.",
                "Via far a lobster drank cockily gazed along a cow grew barring earnest when far tarantula naked some healthy lizard dear gosh a jovially.",
                "Crud danced without apart monkey less equivalent monstrously far hey less far fish editorial and hey indiscriminate peculiar far some far.",
                "This wow and jeez by hey more much hence much dismissively less much did adventurously soggy fixedly hey mastodon.",
                "This improperly octopus less overdrew moth had surreptitious and gnu sold this so as because wetted much worm.",
                "Diversely grandly together earthworm neurotically a turtle until tapir alas ostrich walrus gallantly that some astride some useless memorable octopus alas more on a.",
                "Rooster less upon sold opossum plankton abrupt articulate wherever falcon confessedly more broke less camel koala deer much soberly excepting toucan abashedly.",
                "Jubilantly less solicitously that that across notwithstanding beneath solemn in tiger yet lighted cat misled.",
                "And less leered near much curiously limpet buffalo confessedly wow diabolic with yikes hello a after hedgehog so much goodness much clinic some.",
                "That far and hare with goodness reset belched far leaned egregious the since contrary licentiously this melodiously stormily limpet judicious and won around.",
                "Grimaced bat flamingo covetous far much and regardless iguanodon perceptible alas compactly shined impudent woodpecker.",
                "Limpet less far however opposite prodigious fish gazed vulture far that therefore broadcast robustly much well via far as.",
                "Much spaciously much up flawlessly when via and because far maternal by fitting preparatory regardless yikes.",
                "Loudly clapped as far right perniciously more falcon into consistent cumulatively paid until one less rat vacuous atrociously much therefore one a.",
                "And distant above cold one irresolute excluding overrode bluebird that via wow scratched unobtrusively laboriously."
            };
            Random rand = new Random();
            var maxLength = context.Users.Count();
            int counter = 0;
            foreach (var ideaStr in ideasList)
            {
                var user = context.AppUsers.Local.Skip(rand.Next(0, maxLength - 1)).FirstOrDefault();
                var newDate = DateTime.Now.Subtract(TimeSpan.FromDays(counter));
                context.Ideas.Add(new IdeaEntry()
                {
                    Creator = user,
                    Title = ideaTitles[counter],
                    ShortDescription = ideaStr,
                    FullText = ideaStr+ideaStr+ideaStr,
                    DateCreated = newDate
                });
                counter++;
            }
        }

        private static void FillUsers(ApplicationDbContext context)
        {
            var namesList = new string[]
            {
                "Kizzie Franks",
                "Kaci Concannon",
                "Kimberli Camilleri",
                "Wyatt Daughdrill",
                "Torrie Sease",
                "Renaldo Pecora",
                "Aleen Seal",
                "Simonne Steptoe",
                "Mallory Axelson",
                "Patrice Gadberry",
            };
            UserManager<ApplicationUser> manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            foreach (var name in namesList)
            {
                manager.Create(new ApplicationUser()
                {
                    FirstName = name.Split()[0],
                    LastName = name.Split()[1],
                    Email = $"{name.Replace(' ', '_')}@agile.com",
                    UserName = $"{name.Replace(' ', '_')}@agile.com"
                }, Guid.NewGuid().ToString().Substring(0, 10));
            }

            var adminUser = new ApplicationUser()
            {
                FirstName = "Lev",
                LastName = "Tolstoi",
                Email = "admin@agile.com",
                UserName = "admin@agile.com"

            };
            var res = manager.Create(adminUser, "123456");

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            roleManager.Create(new IdentityRole("Moderator"));
            manager.AddToRole(manager.FindByEmail("admin@agile.com").Id, "Moderator");

        }
    }
}
