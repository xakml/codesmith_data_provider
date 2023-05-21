namespace DeployLX.Licensing.v4
{
	public interface IChange
	{
		event ChangeEventHandler Changed;

		void MakeReadOnly();
	}
}
