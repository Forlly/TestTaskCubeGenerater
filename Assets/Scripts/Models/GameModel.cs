namespace Models
{
    public class GameModel
    {
        public UserPanelModel UserPanelModel;
        public ObjectsPoolModel ObjectsPoolModel;

        public void Init()
        {
            UserPanelModel = new UserPanelModel();
            UserPanelModel.Init();
            ObjectsPoolModel = new ObjectsPoolModel();
            ObjectsPoolModel.Init();
        }
    }
}