using Structurizr;

namespace safelab_c4_model_design
{
    public class SubscriptionBillingComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "SubscriptionBillingComponent";

        public Component subscription_controller { get; private set; }
        public Component plan_controller { get; private set; }
        public Component subscription_service { get; private set; }
        public Component billing_service { get; private set; }
        public Component payment_gateway_adapter { get; private set; }
        public Component billing_repository { get; private set; }

        public SubscriptionBillingComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
        {
            this.c4 = c4;
            this.contextDiagram = contextDiagram;
            this.containerDiagram = containerDiagram;
        }

        public void Generate()
        {
            AddComponents();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }

        private void AddComponents()
        {
            subscription_controller = containerDiagram.rest_api.AddComponent(
                "Subscription Controller",
                "Handles subscription status, plan activation, renewal, and cancellation requests.",
                "ASP.NET Core Controller"
            );

            plan_controller = containerDiagram.rest_api.AddComponent(
                "Plan Controller",
                "Handles requests for available SafeLab plans and plan details.",
                "ASP.NET Core Controller"
            );

            subscription_service = containerDiagram.rest_api.AddComponent(
                "Subscription Service",
                "Applies subscription rules and manages customer access to SafeLab plans.",
                "C# Service"
            );

            billing_service = containerDiagram.rest_api.AddComponent(
                "Billing Service",
                "Coordinates billing operations, invoices, and payment validation.",
                "C# Service"
            );

            payment_gateway_adapter = containerDiagram.rest_api.AddComponent(
                "Payment Gateway Adapter",
                "Integrates SafeLab billing operations with the external payment gateway.",
                "C# Adapter"
            );

            billing_repository = containerDiagram.rest_api.AddComponent(
                "Billing Repository",
                "Reads and writes plans, subscriptions, invoices, and payment records.",
                "C# Repository"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.pharmaceutical_companies.Uses(
                plan_controller,
                "Reviews available plans"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                subscription_controller,
                "Manages service subscription"
            );

            contextDiagram.laboratory_staff.Uses(
                plan_controller,
                "Reviews service plan information"
            );

            contextDiagram.safelab_administrator.Uses(
                plan_controller,
                "Manages available plans"
            );

            contextDiagram.safelab_administrator.Uses(
                subscription_controller,
                "Reviews customer subscriptions"
            );

            plan_controller.Uses(
                subscription_service,
                "Retrieves plan information"
            );

            subscription_controller.Uses(
                subscription_service,
                "Delegates subscription logic"
            );

            subscription_service.Uses(
                billing_service,
                "Requests billing validation"
            );

            billing_service.Uses(
                payment_gateway_adapter,
                "Processes online payments"
            );

            subscription_service.Uses(
                billing_repository,
                "Persists subscription data"
            );

            billing_service.Uses(
                billing_repository,
                "Persists billing records"
            );

            payment_gateway_adapter.Uses(
                contextDiagram.payment_gateway,
                "Sends payment requests",
                "JSON/HTTPS"
            );

            billing_repository.Uses(
                containerDiagram.database,
                "Reads and writes billing data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#0277bd",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            subscription_controller.AddTags(componentTag);
            plan_controller.AddTags(componentTag);
            subscription_service.AddTags(componentTag);
            billing_service.AddTags(componentTag);
            payment_gateway_adapter.AddTags(componentTag);
            billing_repository.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "safelab-component-subscription-billing",
                "Component Diagram - Subscription & Billing Bounded Context (REST API)"
            );

            componentView.Title = "SafeLab - Subscription & Billing";

            componentView.Add(subscription_controller);
            componentView.Add(plan_controller);
            componentView.Add(subscription_service);
            componentView.Add(billing_service);
            componentView.Add(payment_gateway_adapter);
            componentView.Add(billing_repository);

            componentView.Add(contextDiagram.laboratory_staff);
            componentView.Add(contextDiagram.pharmaceutical_companies);
            componentView.Add(contextDiagram.safelab_administrator);
            componentView.Add(contextDiagram.payment_gateway);
            componentView.Add(containerDiagram.database);
        }
    }
}
