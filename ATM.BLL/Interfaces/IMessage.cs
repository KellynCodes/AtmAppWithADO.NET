namespace ATM.BLL.Interfaces
{
    public interface IMessage
    {
        void Error(string message);
        void Danger(string message);
        void Success(string message);
        void Alert(string message);
        void AlertInfo(string message);
    }
}
