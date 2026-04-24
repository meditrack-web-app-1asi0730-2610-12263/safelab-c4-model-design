using Structurizr;
using Structurizr.Api;

namespace safelab_c4_model_design
{
    public class C4
    {
        // Structurizr configuration
        private readonly long workspaceId = 109627;
        private readonly string apiKey = "27ab1802-0514-4cbd-9c04-171f1bdd6832";
        private readonly string apiSecret = "82ef3fef-a16c-422c-bb0d-5c793d115523";

        // C4 Model
        public StructurizrClient StructurizrClient { get; }
        public Workspace Workspace { get; }
        public Model Model { get; }
        public ViewSet ViewSet { get; }

        // Constructor
        public C4()
        {
            // Initialize Structurizr client and workspace
            string workspaceName = "SafeLab - DDD";
            string workspaceDescription = "Domain-Driven Software Architecture for SafeLab application.";

            StructurizrClient = new StructurizrClient(apiKey, apiSecret);
            Workspace = new Workspace(workspaceName, workspaceDescription);
            Model = Workspace.Model;
            ViewSet = Workspace.Views;
        }

        // Generate Method
        public void Generate()
        {
            // Create diagrams
            ContextDiagram contextDiagram =
                new ContextDiagram(this);
    
            ContainerDiagram containerDiagram =
                new ContainerDiagram(this, contextDiagram);
            
            WebApplicationComponentDiagram webApplicationComponentDiagram =
                new WebApplicationComponentDiagram(this, contextDiagram, containerDiagram);
            IdentityAccessComponentDiagram identityAccessComponentDiagram =
                new IdentityAccessComponentDiagram(this, contextDiagram, containerDiagram);
            UserProfilesComponentDiagram userProfilesComponentDiagram =
                new UserProfilesComponentDiagram(this, contextDiagram, containerDiagram);
            SubscriptionBillingComponentDiagram subscriptionBillingComponentDiagram =
                new SubscriptionBillingComponentDiagram(this, contextDiagram, containerDiagram);
            DashboardOverviewComponentDiagram dashboardOverviewComponentDiagram =
                new DashboardOverviewComponentDiagram(this, contextDiagram, containerDiagram);
            AssetInventoryMonitoringComponentDiagram assetInventoryMonitoringComponentDiagram =
                new AssetInventoryMonitoringComponentDiagram(this, contextDiagram, containerDiagram);
            SensorMonitoringComponentDiagram sensorMonitoringComponentDiagram =
                new SensorMonitoringComponentDiagram(this, contextDiagram, containerDiagram);
            
            // Generate diagrams
            contextDiagram.Generate();
            containerDiagram.Generate();
            webApplicationComponentDiagram.Generate();
            identityAccessComponentDiagram.Generate();
            userProfilesComponentDiagram.Generate();
            subscriptionBillingComponentDiagram.Generate();
            dashboardOverviewComponentDiagram.Generate();
            assetInventoryMonitoringComponentDiagram.Generate();
            sensorMonitoringComponentDiagram.Generate();
            
            // Upload workspace to Structurizr
            StructurizrClient.PutWorkspace(workspaceId, Workspace);
        }
    }
}