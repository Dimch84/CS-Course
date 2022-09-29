using System;
using System.Threading;

namespace DelegatesIntro
{
    public delegate void WorldSavedHandler(object sender, WorldSavedEventArgs e);

    public class SuperHero
    {
        public event WorldSavedHandler WorldSaved;
        public event EventHandler<WorldSavingCompletedEventArgs> WorldSavingCompleted;

        public void SaveTheWorld(string saviourName, DateTime dateForNextCatastrophy)
        {
            int hoursToSaveTheWorld = 4;
            for (int i = 0; i < hoursToSaveTheWorld; i++)
            {
                OnWorldSaved(i + 1, saviourName, dateForNextCatastrophy);
                Thread.Sleep(1000);
            }
            OnWorldSavingCompleted(hoursToSaveTheWorld, "Yaaay!", saviourName, dateForNextCatastrophy);
        }

        private void OnWorldSaved(int hoursPassed, string saviourName, DateTime dateForNextCatastrophy)
        {
            if (WorldSaved != null)
            {
                WorldSavedEventArgs e = new WorldSavedEventArgs()
                {
                    DateOfNextCatastrophy = dateForNextCatastrophy
                    , SaviourName = saviourName
                    , WorkHasBeenOngoingHs = hoursPassed
                };
                WorldSaved(this, e);
            }
        }

        private void OnWorldSavingCompleted(int totalHoursWorked, string message, string saviour, DateTime timeOfNextCatastrophy)
        {
            if (WorldSavingCompleted != null)
            {
                WorldSavingCompletedEventArgs e = new WorldSavingCompletedEventArgs()
                {
                    HoursItTookToSaveWorld = totalHoursWorked
                    , MessageFromSaviour = message
                    , Saviour = saviour
                    , TimeForNextCatastrophy = timeOfNextCatastrophy
                };
                WorldSavingCompleted(this, e);
            }
        }
    }
}
