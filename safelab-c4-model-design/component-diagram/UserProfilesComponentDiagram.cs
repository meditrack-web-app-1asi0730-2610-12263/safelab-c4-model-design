using Structurizr;

namespace safelab_c4_model_design
{
    public class UserProfilesComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "ProfilesPreferencesComponent";

        // Components
        public Component profile_controller { get; private set; }
        public Component preferences_controller { get; private set; }
        public Component profile_service { get; private set; }
        public Component preferences_service { get; private set; }
        public Component profile_repository { get; private set; }
        public Component profile_entity { get; private set; }

        // Constructor
        public UserProfilesComponentDiagram(C4 c4, ContextDiagram contextDiagram,
            ContainerDiagram containerDiagram)
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
            profile_controller = containerDiagram.rest_api.AddComponent(
                "Profile Controller",
                "Handles requests for viewing and updating SafeLab user profiles.",
                "ASP.NET Core Controller"
            );

            preferences_controller = containerDiagram.rest_api.AddComponent(
                "Preferences Controller",
                "Handles requests for configuring language, alert, and notification preferences.",
                "ASP.NET Core Controller"
            );

            profile_service = containerDiagram.rest_api.AddComponent(
                "Profile Service",
                "Validates and applies profile updates for laboratory and pharmaceutical users.",
                "C# Service"
            );

            preferences_service = containerDiagram.rest_api.AddComponent(
                "Preferences Service",
                "Applies user preferences for alerts, communication channels, and interface settings.",
                "C# Service"
            );

            profile_repository = containerDiagram.rest_api.AddComponent(
                "Profile Repository",
                "Reads and writes profile and preference data.",
                "C# Repository"
            );

            profile_entity = containerDiagram.rest_api.AddComponent(
                "Profile Entity",
                "Represents user profile data, contact information, preferences, and assigned role.",
                "C# Class"
            );
        }

        // Add Relationships
        private void AddRelationships()
        {
            // People to Components
            contextDiagram.laboratory_staff.Uses(
                profile_controller,
                "Manages personal profile"
            );

            contextDiagram.laboratory_staff.Uses(
                preferences_controller,
                "Configures alert preferences"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                profile_controller,
                "Manages company user profiles"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                preferences_controller,
                "Configures compliance notifications"
            );

            contextDiagram.safelab_administrator.Uses(
                profile_controller,
                "Manages user profile records"
            );

            contextDiagram.safelab_administrator.Uses(
                preferences_controller,
                "Reviews user preference settings"
            );

            // Components to Components
            profile_controller.Uses(
                profile_service,
                "Delegates profile logic"
            );

            preferences_controller.Uses(
                preferences_service,
                "Delegates preference logic"
            );

            profile_service.Uses(
                profile_repository,
                "Persists profile data"
            );

            preferences_service.Uses(
                profile_repository,
                "Persists preference data"
            );

            profile_repository.Uses(
                profile_entity,
                "Maps data to profile model"
            );

            profile_repository.Uses(
                containerDiagram.database,
                "Reads and writes profile data",
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
                Background = "#2e7d32", // green (Profiles & Preferences)
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        // Set Tags
        private void SetTags()
        {
            // Components
            profile_controller.AddTags(componentTag);
            preferences_controller.AddTags(componentTag);
            profile_service.AddTags(componentTag);
            preferences_service.AddTags(componentTag);
            profile_repository.AddTags(componentTag);
            profile_entity.AddTags(componentTag);
        }

        // Create View
        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "safelab-component-profiles-preferences",
                "Component Diagram - Profiles & Preferences Bounded Context (REST API)"
            );

            // Title
            string title = "SafeLab - Profiles & Preferences";
            componentView.Title = title;

            // Elements to add to the view
            componentView.Add(profile_controller);
            componentView.Add(preferences_controller);
            componentView.Add(profile_service);
            componentView.Add(preferences_service);
            componentView.Add(profile_repository);
            componentView.Add(profile_entity);

            // People and external elements to add to the view
            componentView.Add(contextDiagram.laboratory_staff);
            componentView.Add(contextDiagram.pharmaceutical_companies);
            componentView.Add(contextDiagram.safelab_administrator);
            componentView.Add(containerDiagram.database);
        }
    }
}
