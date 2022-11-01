namespace Models
{
    public class GameModel
    {
        public UserPanelModel UserPanelModel;

        public void Init()
        {
            UserPanelModel = new UserPanelModel();
            UserPanelModel.Init();
        }
    }
}