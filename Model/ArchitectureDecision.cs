namespace AnemicModel
{
    public partial class ArchitectureDecision
    {
        public string Consequences { get; set; }
        public string Context { get; set; }
        public DateOnly Date { get; set; }
        public string Decision { get; set; }
        public Status DecisionStatus { get; private set; }
        public ArchitectureDecision? Replacement { get; set; }
        public string Title { get; set; }

        public ArchitectureDecision(
            string consequences,
            string context, 
            DateOnly date,
            string decision, 
            Status status,
            string title,
            ArchitectureDecision? replacement = null)
        {
            Consequences = string.IsNullOrWhiteSpace(consequences)
                ? throw new ArgumentNullException(nameof(consequences))
                : consequences;
            Context = string.IsNullOrWhiteSpace(context) 
                ? throw new ArgumentNullException(nameof(context)) 
                : context;
            Date = date == default 
                ? throw new ArgumentNullException(nameof(date))
                : date;
            Decision = string.IsNullOrWhiteSpace(decision)
                ? throw new ArgumentNullException(nameof(decision))
                : decision;
            DecisionStatus = status;
            Replacement = replacement;
            Title = string.IsNullOrWhiteSpace(title)
                ? throw new ArgumentNullException(nameof(title))
                : title;
            Validate();
        }

        public void Validate()
        {
            if (DecisionStatus == Status.Superseded && Replacement == null)
            {
                throw new ValidationException("Replacement is missing.");
            }
        }
    }
}
