namespace Deviot.Hermes.ModbusTcp.Business.Interfaces
{
    public interface IMigrationService
    {
        public void Execute();

        public void Deleted();

        public void Populate();
    }
}
