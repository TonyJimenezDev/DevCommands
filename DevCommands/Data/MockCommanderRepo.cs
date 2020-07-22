using DevCommands.Models;
using System;
using System.Collections.Generic;

namespace DevCommands.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            List<Command> commands = new List<Command>()
            {
                new Command { Id = 0, HowTo = "Fly", Line = "Gain powers", Platform = "Video Game" },
                new Command { Id = 1, HowTo = "Punch", Line = "Push your arm out with a fist", Platform = "Your Body" },
                new Command { Id = 2, HowTo = "Make coffee", Line = "Place coffee bean", Platform = "Starbucks" }
            };
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Fly", Line = "Gain powers", Platform = "Video Game" };
        }
        #region Not in use interface
        public void CreateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new NotImplementedException();
        }
        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
