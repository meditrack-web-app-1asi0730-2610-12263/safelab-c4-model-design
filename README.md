# SafeLab C4 Model Design

Professional architecture repository for **SafeLab**, a smart monitoring platform focused on monitoring laboratory assets, environmental conditions, alerts, and compliance processes in real time.

This repository contains architecture artifacts developed using the **C4 Model**, **Structurizr**, and **Domain-Driven Design (DDD)**. It documents the system from strategic context level to detailed component level.

---

## Project Overview

SafeLab is a digital platform designed for:

- Hospital laboratories
- Pharmaceutical companies
- Clinical analysis centers
- Controlled storage environments
- Critical equipment monitoring scenarios

The solution connects with sensors and integrated systems to supervise refrigerators, incubators, cold-chain units, storage rooms, and critical supplies.

Main business capabilities:

- Real-time monitoring
- Alerts and notifications
- Equipment status supervision
- Inventory visibility
- Compliance validation
- Incident response
- Reports and analytics
- Remote actuation (when supported)

---

## Repository Objectives

This repository centralizes all software architecture deliverables for SafeLab.

Its purpose is to:

- Define the software architecture clearly
- Maintain diagram consistency
- Represent bounded contexts using DDD
- Support academic documentation
- Serve as technical reference for future implementation
- Enable iterative architecture evolution

---

## Architecture Vision

The architecture is based on modular separation of concerns.

Principles applied:

- Domain-Driven Design (DDD)
- Bounded Context separation
- Clean architecture mindset
- RESTful integration style
- High cohesion / low coupling
- Scalable SaaS structure
- Clear visual documentation through C4

---

## Technology Stack

| Layer | Technology |
|---|---|
| Business Website | HTML5, CSS3, JavaScript |
| Web Application | Vue.js + PrimeVue |
| Backend API | ASP.NET Core with C# |
| Database | SQL Server |
| Mobile App *(optional)* | Flutter |
| Architecture Modeling | Structurizr |
| Source Control | Git + GitHub |

---

## C4 Model Scope

This repository includes diagrams for:

### Level 1 — Context Diagram

Shows:

- Users
- External systems
- SafeLab as a central software system

### Level 2 — Container Diagram

Shows:

- Business Website
- Web Application
- REST API
- Database
- Optional Mobile App

### Level 3 — Component Diagrams

Shows internal structures for bounded contexts such as:

- Identity & Access
- Monitoring
- Alerts
- Reports
- Assets
- Billing

---

## Bounded Contexts

### Generic / Supporting

- Identity & Access Management
- User Profiles
- Subscription & Billing
- Dashboard & Overview

### Core Domain

- Asset & Inventory Monitoring
- Sensor Monitoring
- Environmental Compliance
- Alerts & Notifications
- Remote Control & Actuation
- Reports & Analytics
- Incident Management
- Audit & Traceability

---

## Repository Structure

```text
safelab-c4-model-design/
├── ContextDiagram.cs
├── ContainerDiagram.cs
├── ComponentDiagramIdentityAccess.cs
├── ComponentDiagramMonitoring.cs
├── ComponentDiagramReports.cs
├── Program.cs
├── docs/
│   ├── exports/
│   └── references/
└── README.md
```

---

## Git Workflow

Recommended conventions:

### Branching

- main
- develop
- feature/*

### Conventional Commits

Examples:

```text
feat: add container diagram for SafeLab
docs: update README architecture section
refactor: simplify identity access components
chore: reorganize structurizr exports
```

---

## Roadmap

Planned next steps:

- Complete component diagrams for all BCs
- Add UML class diagrams
- Add database ER diagrams
- Connect diagrams with implementation repository
- Add deployment architecture view
- Add sequence diagrams for critical flows

---

## Contributors

Developed by the SafeLab project team.

---

## License

For academic and educational use unless otherwise specified.
