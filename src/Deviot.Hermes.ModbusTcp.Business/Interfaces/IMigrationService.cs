namespace Deviot.Hermes.ModbusTcp.Business.Interfaces
{
    public interface IMigrationService
    {
        public void DeleteDatabase();

        public void Execute();
    }
}
