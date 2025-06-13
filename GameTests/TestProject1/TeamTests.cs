using MinuteBattle.Logic;

namespace TestProject1
{
    public class TeamTests
    {
        [SetUp]
        public void Setup()
        {
            GameDefinitions._teamSideList.Clear();
        }
        [Test]
        public void AddingTeamSide()
        {
            //TeamSide is the different fighting sides 
            const int CONST_TEAM_SIDE_ID = 1;
            const string CONST_TEAM_SIDE_NAME = "Allies";
            var teamSide = new TeamSide(CONST_TEAM_SIDE_ID, CONST_TEAM_SIDE_NAME);
            GameDefinitions.AddTeamSide(CONST_TEAM_SIDE_ID, teamSide);
            Assert.IsTrue(GameDefinitions.GetTeamSide(CONST_TEAM_SIDE_ID)._name == CONST_TEAM_SIDE_NAME);
        }
        [Test]
        public void AddingTeam()
        {
            //Team is the configuration of the teams belonging to the TeamSides
            //It is the startup settings of the different teams
            const int CONST_TEAM_SIDE_ID = 1;
            const string CONST_TEAM_SIDE_NAME = "Allies";
            var teamSide = new TeamSide(CONST_TEAM_SIDE_ID, CONST_TEAM_SIDE_NAME);
            GameDefinitions.AddTeamSide(CONST_TEAM_SIDE_ID, teamSide);
            const int CONST_TEAM_ID = 1;
            const string CONST_TEAM_NAME = "Brittish";
            const float CONST_TEAM_GOLD = 1000;
            const float CONST_TEAM_XP = 200;
            const float CONST_RESOURCE_REINFORCEMENTS = 120;
            const float CONST_RESOURCE_CAMP = 50;
            const float CONST_RESOURCE_FRONTLINE = 56;
            const float CONST_RESOURCE_MOVEMENT_SPEED = 12;
            const float CONST_RESOURCE_REINFORCEMENTS_MAX = 500;
            const float CONST_RESOURCE_CAMP_MAX = 90;
            const float CONST_RESOURCE_FRONTLINE_MAX = 120;
            const float CONST_RESOURCE_MOVEMENT_SPEED_MAX = 45;
            teamSide.AddTeam(
                CONST_TEAM_ID, 
                new Team(
                    CONST_TEAM_ID, 
                    CONST_TEAM_NAME, 
                    new Resources(
                        CONST_RESOURCE_REINFORCEMENTS_MAX,
                        CONST_RESOURCE_CAMP_MAX,
                        CONST_RESOURCE_FRONTLINE_MAX,
                        CONST_RESOURCE_MOVEMENT_SPEED_MAX
                    ),
                    new Resources(
                        CONST_RESOURCE_REINFORCEMENTS,
                        CONST_RESOURCE_CAMP,
                        CONST_RESOURCE_FRONTLINE,
                        CONST_RESOURCE_MOVEMENT_SPEED
                    ),
                    CONST_TEAM_GOLD, 
                    CONST_TEAM_XP
                )
            );
            Assert.IsTrue(teamSide.GetTeam(CONST_TEAM_ID)._name == CONST_TEAM_NAME);
            Assert.IsTrue(teamSide.GetTeam(CONST_TEAM_ID)._resourcesLevel._reinforcements == CONST_RESOURCE_REINFORCEMENTS);
            Assert.IsTrue(teamSide.GetTeam(CONST_TEAM_ID)._resourcesLevel._camp == CONST_RESOURCE_CAMP);
            Assert.IsTrue(teamSide.GetTeam(CONST_TEAM_ID)._resourcesLevel._frontline == CONST_RESOURCE_FRONTLINE);
            Assert.IsTrue(teamSide.GetTeam(CONST_TEAM_ID)._resourcesLevel._movementSpeed == CONST_RESOURCE_MOVEMENT_SPEED);
            Assert.IsTrue(teamSide.GetTeam(CONST_TEAM_ID)._resourcesMax._reinforcements == CONST_RESOURCE_REINFORCEMENTS_MAX);
            Assert.IsTrue(teamSide.GetTeam(CONST_TEAM_ID)._resourcesMax._camp == CONST_RESOURCE_CAMP_MAX);
            Assert.IsTrue(teamSide.GetTeam(CONST_TEAM_ID)._resourcesMax._frontline == CONST_RESOURCE_FRONTLINE_MAX);
            Assert.IsTrue(teamSide.GetTeam(CONST_TEAM_ID)._resourcesMax._movementSpeed == CONST_RESOURCE_MOVEMENT_SPEED_MAX);
            Assert.IsTrue(teamSide.GetTeam(CONST_TEAM_ID)._gold == CONST_TEAM_GOLD);
            Assert.IsTrue(teamSide.GetTeam(CONST_TEAM_ID)._xp == CONST_TEAM_XP);
        }
        [Test]
        public void AddingCard()
        {
            //Card is the configuration of the cards belonging to a team
            //It is the default start settings of the different cards
            const int CONST_TEAM_SIDE_ID = 1;
            const string CONST_TEAM_SIDE_NAME = "Allies";
            var teamSide = new TeamSide(CONST_TEAM_SIDE_ID, CONST_TEAM_SIDE_NAME);
            GameDefinitions.AddTeamSide(CONST_TEAM_SIDE_ID, teamSide);
            const int CONST_TEAM_ID = 1;
            const string CONST_TEAM_NAME = "Brittish";
            var team = new Team(CONST_TEAM_ID, CONST_TEAM_NAME, new Resources(0, 0, 0, 0), new Resources(0, 0, 0, 0), 0, 0);
            teamSide.AddTeam(CONST_TEAM_ID, team);
            Assert.IsTrue(teamSide.GetTeam(CONST_TEAM_ID)._name == CONST_TEAM_NAME);
            const int CONST_CARD_ID = 1;
            const string CONST_CARD_NAME = "Private";
            const string CONST_CARD_DESCRIPTION = "Single shot soldier";
            const float CONST_ATTRIBUTES_HEALTH = 1.0f;
            const float CONST_ATTRIBUTES_MOVEMENT_SPEED = 2.0f;
            const float CONST_ATTRIBUTES_FIRE_RATE = 3.0f;
            const float CONST_ATTRIBUTES_FIRE_RANGE = 4.0f;
            const float CONST_ATTRIBUTES_ACCURACY = 5.0f;
            const float CONST_ATTRIBUTES_DAMAGE = 6.0f;
            team.AddCardToDeck(CONST_CARD_ID, 
                new Card(CONST_CARD_ID, CONST_CARD_NAME, CONST_CARD_DESCRIPTION, 
                    new Attributes(
                        CONST_ATTRIBUTES_HEALTH,
                        CONST_ATTRIBUTES_MOVEMENT_SPEED,
                        CONST_ATTRIBUTES_FIRE_RATE,
                        CONST_ATTRIBUTES_FIRE_RANGE,
                        CONST_ATTRIBUTES_ACCURACY,
                        CONST_ATTRIBUTES_DAMAGE
                    )
                )
             );
            Assert.IsTrue(team.GetCardFromDeck(CONST_CARD_ID)._name == CONST_CARD_NAME);
            Assert.IsTrue(team.GetCardFromDeck(CONST_CARD_ID)._description == CONST_CARD_DESCRIPTION);
            Assert.IsTrue(team.GetCardFromDeck(CONST_CARD_ID)._attributes._health == CONST_ATTRIBUTES_HEALTH);
            Assert.IsTrue(team.GetCardFromDeck(CONST_CARD_ID)._attributes._movementSpeed == CONST_ATTRIBUTES_MOVEMENT_SPEED);
            Assert.IsTrue(team.GetCardFromDeck(CONST_CARD_ID)._attributes._fireRate == CONST_ATTRIBUTES_FIRE_RATE);
            Assert.IsTrue(team.GetCardFromDeck(CONST_CARD_ID)._attributes._fireRange == CONST_ATTRIBUTES_FIRE_RANGE);
            Assert.IsTrue(team.GetCardFromDeck(CONST_CARD_ID)._attributes._accuracy == CONST_ATTRIBUTES_ACCURACY);
            Assert.IsTrue(team.GetCardFromDeck(CONST_CARD_ID)._attributes._damage == CONST_ATTRIBUTES_DAMAGE);
        }
    }
}