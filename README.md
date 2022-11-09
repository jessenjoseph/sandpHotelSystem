# sandpHotelSystem
Nov 22 Hotel Booking 


Hotel Bookings Problem 
A simple hotel booking system keeps track of the rooms in a hotel. A guest can book a room for individual nights and the booking system maintains the state of these bookings. 
•	Guests are identified by their surname which, for the purposes of this exercise, can be considered unique. 
•	Rooms are identified by a unique number taken from an arbitrary, potentially nonsequential set of numbers. For example, a hotel might have four rooms {101, 102, 201, 203}. 
•	The booking system may be used by a number of hotel staff at once, so your implementation must be thread-safe. 
•	There is no need to write an interactive harness or command-line interface. Simply call your code from a main method or unit test to demonstrate it works. 
•	There is no need to implement a persistent store. 

==============================================
SOLUTION

BookingSystemOrchestrator.cs is the starting point for the BookingSystem 
  a. Initialises the cache for the given Date
  b. Adds Booking if Room is available for the given Date
  
If there was more time, I would have added better ExceptionHandling
  
TestClasses added for BookingManager & HotelDataRepository
