using AnemicModel;

using Structurizr;
using Structurizr.Documentation;

namespace Infrastructure.Structurizr.Handlers.Commands.AppService
{
    public static partial class SaveViewCommandHandler
    {
        private static DecisionStatus GetStatus(ArchitectureDecision decision, DecisionStatus decisionStatus)
        {
            switch (decision.DecisionStatus)
            {
                case ArchitectureDecision.Status.Accepted:
                    decisionStatus = DecisionStatus.Accepted;
                    break;
                case ArchitectureDecision.Status.Deprecated:
                    decisionStatus = DecisionStatus.Deprecated;
                    break;
                case ArchitectureDecision.Status.Proposed:
                    decisionStatus = DecisionStatus.Proposed;
                    break;
                case ArchitectureDecision.Status.Superseded:
                    decisionStatus = DecisionStatus.Superseded;
                    break;
            }

            return decisionStatus;
        }
        public static void HandleDecisions(
            IEnumerable<ArchitectureDecision>? architectureDecisions, Workspace workspace)
        {
            if (architectureDecisions == null)
            {
                return;
            }

            for (int i = 0; i < architectureDecisions.Count(); i++)
            {
                ArchitectureDecision decision = architectureDecisions.ElementAt(i);
                string id = $"{i + 1}";

                string context = $"Context: {decision.Context}, Decision: {decision.Decision}, " +
                    $"Consequences: {decision.Consequences}{(decision.DecisionStatus == ArchitectureDecision.Status.Superseded ? nameof(ArchitectureDecision.Status.Superseded) + ": " + decision.Replacement.Title : String.Empty)}";

                DateTime date = decision.Date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);

                DecisionStatus decisionStatus = default;
                decisionStatus = GetStatus(decision, decisionStatus);
                workspace.Documentation.AddDecision(
                    id,
                    date,
                    decision.Title,
                    decisionStatus,
                    Format.Markdown,
                    context);
            }
        }

    }
}
