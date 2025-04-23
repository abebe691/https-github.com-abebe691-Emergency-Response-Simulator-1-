using System;
using System.Collections.Generic;
namespace EmergencyResponseSimulator
{
    abstract class EmergencyUnit
    {
        public string Name { get; private set; }
        public int Speed { get; private set; }

        public EmergencyUnit(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }

        public abstract bool CanHandle(string incidentType);
        public abstract void RespondToIncident(Incident incident);
    }

    class Police : EmergencyUnit
    {
        public Police(string name, int speed) : base(name, speed) { }

        public override bool CanHandle(string incidentType) => incidentType == "Crime";

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is handling a crime at {incident.Location}.");
        }
    }

    class Firefighter : EmergencyUnit
    {
        public Firefighter(string name, int speed) : base(name, speed) { }

        public override bool CanHandle(string incidentType) => incidentType == "Fire";

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is extinguishing a fire at {incident.Location}.");
        }
    }

    class Ambulance : EmergencyUnit
    {
        public Ambulance(string name, int speed) : base(name, speed) { }

        public override bool CanHandle(string incidentType) => incidentType == "Medical";

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is treating patients at {incident.Location}.");
        }
    }

    class Incident
    {
        public string Type { get; private set; }
        public string Location { get; private set; }

        public Incident(string type, string location)
        {
            Type = type;
            Location = location;
        }
    }

    class Program
    {
        static void Main()
        {
            List<EmergencyUnit> units = new List<EmergencyUnit>
            {
                new Police("Police Unit 1", 5),
                new Firefighter("Firefighter Unit 1", 7),
                new Ambulance("Ambulance Unit 1", 8)
            };

            string[] incidentTypes = { "Crime", "Fire", "Medical" };
            string[] locations = { "City Hall", "Downtown", "Park", "Mall", "University" };

            int score = 0;

            Console.WriteLine("** Emergency Response Simulator **");
            Console.WriteLine("Simulation starts now...\n");

            for (int round = 1; round <= 5; round++)
            {
                Console.WriteLine($"--- Turn {round} ---");

                string randomIncidentType = incidentTypes[new Random().Next(incidentTypes.Length)];
                string randomLocation = locations[new Random().Next(locations.Length)];

                Incident incident = new Incident(randomIncidentType, randomLocation);
                Console.WriteLine($"Incident: {incident.Type} at {incident.Location}");

                EmergencyUnit responder = units.Find(unit => unit.CanHandle(incident.Type));

                if (responder != null)
                {
                    responder.RespondToIncident(incident);
                    score += 10;
                }
                else
                {
                    Console.WriteLine("No emergency unit available for this incident.");
                    score -= 5;
                }

                Console.WriteLine($"Current Score: {score}\n");
            }

            Console.WriteLine("Simulation Ended.");
            Console.WriteLine($"Final Score: {score}");
        }
    }
}
