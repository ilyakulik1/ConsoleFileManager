namespace ConsoleFileManager.Serialization
{
    public interface ISerialize
    {
        void Serialize(SerializeModel model);
        SerializeModel Deserialize();
    }
}
