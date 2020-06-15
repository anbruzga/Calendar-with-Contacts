# Smart-Calendar-With-Contacts
 Calendar where user can set optionally recurring events, contacts, events with contacts, predict future time usage by extrapolation. For Windows.

## Functionality

The calendar uses WinForms to display allow the user to:  
1. Enter new, modify and delete contacts.
2. Enter, modify and delete events. The events can be recurring in many different ways.
2.1. Events can be appointments. In that case, the user can select the contact.
3. All the event modifications are updated in fully programmatical calendar view upon adding/modifying/deleting event.
4. Selecting particular slot in the calendar user gets events of that day. User can select to edit/delete events there.
5. Deleting/modifying recurring events modify all the instances of it, no matter how it repeats.
6. The user can scroll the calendar months.
7. The user can predict future time. The algorithm determines if it is best to take last 3 months, last 2 months or last 1 month and performs the calculation. The calculation is performed by converting each day to boolean array representing minutes. Then, each busy minute gets value true and the ratio is counted. That is implemented to allow overlapping events and to prevent extrapolation returning more than 24hrs of busy time.


## Example of calendar view adding event:
![Screenshot1](/Screenshots/CalExample1.PNG)

## Example of editing/deleting contacts:
![Screenshot1](/Screenshots/CalExample2.PNG)

## Example of future time prediction:
![Screenshot1](/Screenshots/CalExample3.PNG)

## Example of contact menu:
![Screenshot1](/Screenshots/CalExample4.PNG)
