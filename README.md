# BookingApp# Resource Booking Application

## Overview

This is a full-featured ASP.NET Core MVC web application designed for managing resources and bookings. It allows users to create, edit, view, and delete resources, as well as make bookings for these resources with proper validation to avoid scheduling conflicts.

The application features:

- Resource management (Create, Read, Update, Delete - CRUD)
- Booking management with date/time validation and conflict checks
- Responsive and modern UI powered by Bootstrap 5 and Font Awesome icons
- Advanced data tables with search, sorting, and pagination for easy data navigation
- Modal popups for confirmation dialogs to enhance user experience
- Real-time feedback with success/error messages
- Entity Framework Core integration with a SQL database backend
- Secure form handling with anti-forgery tokens

---

## Features

### Resources

- Add new resources with detailed information such as name, description, location, capacity, and availability status.
- Edit existing resource details.
- Delete resources with modal confirmation to prevent accidental deletion.
- View a paginated, searchable, and sortable list of resources.

### Bookings

- Create bookings for resources with start and end date/time.
- Prevent overlapping bookings through conflict validation.
- Edit and delete bookings.
- View booking details, including resource information and booking purpose.
- Booking list is searchable and paginated for better usability.

---

## Prerequisites

- [.NET 8 SDK or later](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or any compatible IDE
- SQL Server Express LocalDB (or any SQL Server instance)
- Internet connection (for CDN resources like Bootstrap and Font Awesome)

---

## Getting Started

1. **Clone the Repository**

```bash
git clone https://github.com/malangwane/ResourceBookingApp.git
cd ResourceBookingApp
