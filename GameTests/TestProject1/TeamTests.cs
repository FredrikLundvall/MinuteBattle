using MinuteBattle.Logic;

namespace TestProject1
{
    public class TeamTests
    {
        [SetUp]
        public void Setup()
        {
            GameDefinitions._teamSideList.Clear();
            GameDefinitions._teamDefinitionList.Clear();
        }
        [Test]
        public void AddingTeamSide()
        {
            const int CONST_TEAM_SIDE_ID = 1;
            const string CONST_TEAM_SIDE_NAME = "Allies";
            GameDefinitions.AddTeamSide(CONST_TEAM_SIDE_ID, new TeamSide(CONST_TEAM_SIDE_ID, CONST_TEAM_SIDE_NAME));
            Assert.IsTrue(GameDefinitions.GetTeamSide(CONST_TEAM_SIDE_ID)._name == CONST_TEAM_SIDE_NAME);
        }
        [Test]
        public void AddingTeamDefinitions()
        {
            const int CONST_TEAM_SIDE_ID = 1;
            const string CONST_TEAM_SIDE_NAME = "Allies";
            var teamSide = new TeamSide(CONST_TEAM_SIDE_ID, CONST_TEAM_SIDE_NAME);
            GameDefinitions.AddTeamSide(CONST_TEAM_SIDE_ID, teamSide);
            const int CONST_TEAM_ID = 1;
            const string CONST_TEAM_NAME = "Brittish";
            GameDefinitions.AddTeamDefinition(CONST_TEAM_ID, new TeamDefinition(teamSide, CONST_TEAM_ID, CONST_TEAM_NAME));
            Assert.IsTrue(GameDefinitions.GetTeamDefinition(CONST_TEAM_ID)._name == CONST_TEAM_NAME);
        }
    }
}