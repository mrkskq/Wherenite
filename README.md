# Wherenite – Event Discovery and Ticketing Platform

## Project Overview
Wherenite (Where tonight) is a web-based application developed in C# ASP.NET Framework using the MVC architecture and Entity Framework for database management. This project is designed to help users discover and follow current music events in Ohrid. It allows users to browse events, filter them by location, venue type, or artist type, and view detailed information such as date, ticket prices, and sample songs from performers. Users can purchase tickets directly through the shopping cart for events that support online sales or make reservations via contact numbers for others. By implementing role-based access, the application ensures smooth and organized event browsing, ticket purchasing, and efficient event management.

## Technologies 
- <b>ASP.NET MVC</b> - web application framework
- <b>C#</b> - for backend logic
- <b>Entity Framework</b> - Object-Relational Mapper (ORM) for database interaction
- <b>HTML, CSS</b> - frontend technologies for structure and styling
- <b>Bootstrap</b> - frontend framework for responsive design
- <b>jQuery</b> - for handling dynamic elements like DataTables and AJAX calls
- <b>Bootbox.js</b> - library used for confirmation dialogs
- <b>SQL Server</b> - for database system
- <b>Stripe API</b> - for payment processing

## User Roles 
### Viewer: 
- Can browse the complete list of all events.
- Can filter events by venue type, specific venue, or artist type.
- Can search for events by venue, artist, date, artist type or by ticket price using the search bar.
- Can view the detailed page for any event, including information like artist details, date, location, ticket categories and prices.
- Can't add tickets to the shopping cart.
- Can't complete a purchase.

### User: 
- Has all the permissions of a viewer.
- Must register or log in to access additional features, which include:
  - Adding tickets to the shopping cart by selecting a ticket category and quantity.
  - Viewing their personal shopping cart with all added items.
  - Completing the purchase process securely via Stripe Checkout integration.

### Admin:
- Has full access to manage all data and system functionalities.
- Can create, edit, delete and manage events, venues and venue types.
- Can manage ticket categories and their prices.
- Can utilize the "Add song to artist" feature to link YouTube videos.

## Features
- <b>Navigation Bar:</b> Includes links to Home, About us, Events, Shopping cart, Venue, Venue type, Register and Login pages.
- <b>Event Filtering:</b> Viewers and users can filter events based on venue type, venue, or artist type.
- <b>Event Details:</b> Each event includes detailed information such as artist type, ticket categories and their prices or contact numbers for phone reservations, date, venue type and venue.
- <b>Dual Booking System:</b>
  - <b>Phone Reservations:</b> For events without online ticketing, users can view contact numbers for direct phone reservations.
  - <b>Online Ticketing:</b> For events with online ticketing, users can choose a ticket category and quantity by pressing "Add to cart" button, and then they can either delete it from the cart or proceed with checkout via secure Stripe checkout integration.
- <b>Adding song to an artist:</b> An admin can add a specific song to a selected artist from a dropdown which includes all artists from the database, and then that song will be displayed as a YouTube video on the artist's details page.
- <b>Authorization:</b> Role-based access ensures that viewers, users and admins each have distinct permissions.

## Ticket Purchasing & Payment Flow
1. Users add events to their cart by pressing "Add to cart" button, where they select the ticket category and quantity. This feature is only available for upcoming events - the button is automatically disabled for past events.
2. The cart displays the tickets, which include details about the artist's name, location, date, quantity, and total price.
3. Users can delete items from the cart (with Bootbox confirmation) or proceed to the Stripe checkout payment page.
4. Testing Payment: Use Stripe's test card `4242 4242 4242 4242` with any future expiration date and any 3-digit CVC. This allows safe simulation and successful payments in the sandbox environment.


