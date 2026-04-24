using Structurizr;

namespace safelab_c4_model_design
{
    public class IdentityAccessComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "IdentityAccessComponent";

        // Components
        public Component auth_controller { get; private set; }
        public Component auth_service { get; private set; }
        public Component user_controller { get; private set; }
        public Component user_repository { get; private set; }
        public Component token_service { get; private set; }

        // Constructor
        public IdentityAccessComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
        {
            this.c4 = c4;
            this.contextDiagram = contextDiagram;
            this.containerDiagram = containerDiagram;
        }

        // Generate Method
        public void Generate()
        {
            AddComponents();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }

        // Add Components
        private void AddComponents()
        {
            auth_controller = containerDiagram.rest_api.AddComponent(
                "Auth Controller",
                "Handles login, logout, and access validation requests.",
                "ASP.NET Core Controller"
            );

            auth_service = containerDiagram.rest_api.AddComponent(
                "Auth Service",
                "Coordinates authentication logic and validates user credentials.",
                "C# Service"
            );

            user_controller = containerDiagram.rest_api.AddComponent(
                "User Controller",
                "Handles user account, role, and permission management requests.",
                "ASP.NET Core Controller"
            );

            user_repository = containerDiagram.rest_api.AddComponent(
                "User Repository",
                "Reads and writes user, role, and permission data.",
                "C# Repository"
            );

            token_service = containerDiagram.rest_api.AddComponent(
                "Token Service",
                "Generates, validates, and refreshes JWT access tokens.",
                "C# Service"
            );
        }

        // Add Relationships
        private void AddRelationships()
        {
            // People to Components
            contextDiagram.laboratory_staff.Uses(
                auth_controller,
                "Authenticates to access SafeLab"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                auth_controller,
                "Authenticates to access SafeLab"
            );

            contextDiagram.safelab_administrator.Uses(
                auth_controller,
                "Authenticates and manages access"
            );

            contextDiagram.safelab_administrator.Uses(
                user_controller,
                "Manages users, roles, and permissions"
            );

            // Components to Components
            auth_controller.Uses(
                auth_service,
                "Delegates authentication logic"
            );

            auth_service.Uses(
                user_repository,
                "Validates credentials and retrieves user roles"
            );

            auth_service.Uses(
                token_service,
                "Generates and validates JWT tokens"
            );

            user_controller.Uses(
                user_repository,
                "Manages identity data"
            );

            // Components to Containers
            user_repository.Uses(
                containerDiagram.database,
                "Reads and writes identity data",
                "SQL"
            );
        }

        // Apply Styles
        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            // Components
            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#6a1b9a",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        // Set Tags
        private void SetTags()
        {
            // Components
            auth_controller.AddTags(componentTag);
            auth_service.AddTags(componentTag);
            user_controller.AddTags(componentTag);
            user_repository.AddTags(componentTag);
            token_service.AddTags(componentTag);
        }

        // Create View
        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "safelab-component-identity-access",
                "Component Diagram - Identity & Access Management Bounded Context (REST API)"
            );

            // Title
            string title = "SafeLab - Identity & Access Management";
            componentView.Title = title;

            // Elements to add to the view
            componentView.Add(auth_controller);
            componentView.Add(auth_service);
            componentView.Add(user_controller);
            componentView.Add(user_repository);
            componentView.Add(token_service);

            // People and external elements to add to the view
            componentView.Add(contextDiagram.laboratory_staff);
            componentView.Add(contextDiagram.pharmaceutical_companies);
            componentView.Add(contextDiagram.safelab_administrator);
            componentView.Add(containerDiagram.database);
        }
    }
}
