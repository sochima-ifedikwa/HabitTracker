# 📌 Habit Tracker Pro — .NET Blazor Project

## 📖 Project Overview
Habit Tracker Pro is a sophisticated web application built with **ASP.NET Core Blazor** designed to help users cultivate meaningful routines through data-driven tracking and an intuitive Kanban-style workflow. 

This project demonstrates a deep integration of Blazor Server capabilities, Entity Framework Core, and modern UI/UX principles to deliver a premium productivity tool.

---

## 🚀 Key Features
- **📊 Dynamic Dashboard**: Real-time statistical overview including completion rates, active habits, and current streaks.
- **📋 Interactive Pipeline**: A full-featured Kanban board with drag-and-drop status management.
- **✅ Smart Habit Management**: Effortless CRUD operations with inline editing and multi-frequency support.
- **🔐 Secure Authentication**: Robust user isolation using ASP.NET Identity.
- **🔔 Real-time Feedback**: Global notification system (Toasts) for all user actions.
- **📱 Responsive & Aesthetic**: Premium "Classic-Modern" design with glassmorphism, animations, and full mobile compatibility.

---

## 🛠️ Technical Implementation (Advanced Patterns)
- **State Management**: Reactive UI updates using cascading parameters and event-driven services.
- **Service Layer**: Clean separation of concerns with dedicated services for Habits, Logs, Dashboard, and Notifications.
- **Error Handling**: Comprehensive Try/Catch blocks at both API and UI levels with user-friendly error reporting.
- **Dependency Injection**: Full utilization of Scoped and Singleton services for efficient resource management.
- **Database Architecture**: EF Core with MySql/SQLite support, implementing optimized queries for historical logging.

---

## ♿ Accessibility & UX (WCAG 2.1 Level AA)
- **Semantic HTML**: Proper use of `<main>`, `<nav>`, `<header>`, and heading hierarchies.
- **ARIA Compliance**: Extensive use of `aria-label`, `aria-live` for toasts, and `role` attributes for interactive components.
- **Keyboard Navigation**: Fully navigable via keyboard with distinct focus states.
- **Contrast & Typography**: High-contrast color palette (Deep Navy / Gold) paired with optimized serif typography for readability.

---

## 👥 Development Team
- **Sochima Ifedikwa** (Project Lead, Backend Architect)
- **Opong Ebenezer Jules Samu Tro** (UI/UX Designer, Frontend Engineer)

---

## 🗂️ Project Workspace
- **Trello Board**: [View Kanban Workflow & Tasks](https://trello.com/b/kOeOa2FE/habit-tracker-net-blazor-project)
- **Repository**: Managed on GitHub with feature-branch workflow.

---

## ⚙️ Setup Instructions
1. Clone the repository.
2. Ensure you have the .NET 8 SDK installed.
3. Update `appsettings.json` with your MySQL connection string (or use the included SQLite development database).
4. Run `dotnet ef database update`.
5. Execute `dotnet run` and navigate to `https://localhost:5001`.
