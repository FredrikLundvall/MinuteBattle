using MinuteBattle.Logic;

namespace TestProject1
{
    public class TeamTests
    {
        [SetUp]
        public void Setup()
        {
            GameDefinitions._teamSideList.Clear();
            GameDefinitions._teamSetupList.Clear();
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
        public void AddingTeamSetup()
        {
            //TeamSetup is the configuration of the teams belonging to the TeamSides
            //It is the startup settings of the different teams
            const int CONST_TEAM_SIDE_ID = 1;
            const string CONST_TEAM_SIDE_NAME = "Allies";
            var teamSide = new TeamSide(CONST_TEAM_SIDE_ID, CONST_TEAM_SIDE_NAME);
            GameDefinitions.AddTeamSide(CONST_TEAM_SIDE_ID, teamSide);
            const int CONST_TEAM_SETUP_ID = 1;
            const string CONST_TEAM_SETUP_NAME = "Brittish";
            const float CONST_TEAM_SETUP_GOLD = 1000;
            const float CONST_TEAM_SETUP_XP = 200;
            const float CONST_RESOURCE_REINFORCEMENTS = 120;
            const float CONST_RESOURCE_CAMP = 50;
            const float CONST_RESOURCE_FRONTLINE = 56;
            const float CONST_RESOURCE_MOVEMENT_SPEED = 12;
            const float CONST_RESOURCE_REINFORCEMENTS_MAX = 500;
            const float CONST_RESOURCE_CAMP_MAX = 90;
            const float CONST_RESOURCE_FRONTLINE_MAX = 120;
            const float CONST_RESOURCE_MOVEMENT_SPEED_MAX = 45;
            GameDefinitions.AddTeamSetup(
                CONST_TEAM_SETUP_ID, 
                new TeamSetup(
                    teamSide, 
                    CONST_TEAM_SETUP_ID, 
                    CONST_TEAM_SETUP_NAME, 
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
                    CONST_TEAM_SETUP_GOLD, 
                    CONST_TEAM_SETUP_XP
                )
            );
            Assert.IsTrue(GameDefinitions.GetTeamSetup(CONST_TEAM_SETUP_ID)._name == CONST_TEAM_SETUP_NAME);
            Assert.IsTrue(GameDefinitions.GetTeamSetup(CONST_TEAM_SETUP_ID)._resourcesLevel._reinforcements == CONST_RESOURCE_REINFORCEMENTS);
            Assert.IsTrue(GameDefinitions.GetTeamSetup(CONST_TEAM_SETUP_ID)._resourcesLevel._camp == CONST_RESOURCE_CAMP);
            Assert.IsTrue(GameDefinitions.GetTeamSetup(CONST_TEAM_SETUP_ID)._resourcesLevel._frontline == CONST_RESOURCE_FRONTLINE);
            Assert.IsTrue(GameDefinitions.GetTeamSetup(CONST_TEAM_SETUP_ID)._resourcesLevel._movementSpeed == CONST_RESOURCE_MOVEMENT_SPEED);
            Assert.IsTrue(GameDefinitions.GetTeamSetup(CONST_TEAM_SETUP_ID)._resourcesMax._reinforcements == CONST_RESOURCE_REINFORCEMENTS_MAX);
            Assert.IsTrue(GameDefinitions.GetTeamSetup(CONST_TEAM_SETUP_ID)._resourcesMax._camp == CONST_RESOURCE_CAMP_MAX);
            Assert.IsTrue(GameDefinitions.GetTeamSetup(CONST_TEAM_SETUP_ID)._resourcesMax._frontline == CONST_RESOURCE_FRONTLINE_MAX);
            Assert.IsTrue(GameDefinitions.GetTeamSetup(CONST_TEAM_SETUP_ID)._resourcesMax._movementSpeed == CONST_RESOURCE_MOVEMENT_SPEED_MAX);
            Assert.IsTrue(GameDefinitions.GetTeamSetup(CONST_TEAM_SETUP_ID)._gold == CONST_TEAM_SETUP_GOLD);
            Assert.IsTrue(GameDefinitions.GetTeamSetup(CONST_TEAM_SETUP_ID)._xp == CONST_TEAM_SETUP_XP);
        }
        [Test]
        public void AddingUnitSetup()
        {
            //UnitSetup is the configuration of the units belonging to a team
            //It is the startup settings of the different units
            const int CONST_TEAM_SIDE_ID = 1;
            const string CONST_TEAM_SIDE_NAME = "Allies";
            var teamSide = new TeamSide(CONST_TEAM_SIDE_ID, CONST_TEAM_SIDE_NAME);
            GameDefinitions.AddTeamSide(CONST_TEAM_SIDE_ID, teamSide);
            const int CONST_TEAM_SETUP_ID = 1;
            const string CONST_TEAM_SETUP_NAME = "Brittish";
            GameDefinitions.AddTeamSetup(CONST_TEAM_SETUP_ID, new TeamSetup(teamSide, CONST_TEAM_SETUP_ID, CONST_TEAM_SETUP_NAME, new Resources(0, 0, 0, 0), new Resources(0, 0, 0, 0), 0, 0));
            Assert.IsTrue(GameDefinitions.GetTeamSetup(CONST_TEAM_SETUP_ID)._name == CONST_TEAM_SETUP_NAME);
        }
    }
}