using System;

public interface ICommand {
	void Execute(IMessage message);
}