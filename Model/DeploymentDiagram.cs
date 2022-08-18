namespace AnemicModel
{
    public class DeploymentDiagram : Diagram
    {
        public IEnumerable<DeploymentBlock>? Blocks { get; set; }

        public DeploymentDiagram(
            string name, IEnumerable<IEnumerable<SoftwareProcess>> processesGroups) : base(
                name, processesGroups)
        {
        }
    }
}
