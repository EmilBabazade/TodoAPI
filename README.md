# TODO API
Created using .Net 6 and Mediatr CQRS

## Try it out
https://todoapi4.herokuapp.com/swagger/index.html

## Swagger
Run the API in Visual Studio or run from terminal and go to https://localhost:7213/swagger to see the swagger docs

## Entities

### User
user

### TODO 
Todo item

## Process
Authorization and simple crud for TODO

## Authorization
JWT was used for authorization. The key can be seen in apsettings ( normally i would put it in user secrets normally but this ain't a real world program )

## Testing
I have only written tests for 2 controller handlers, since the rest of handlers have more or less same testing scenarios, i didn't want to copy paste the same thing over and over since this is a toy api ( there would be more logic to each handler and it would be very critical to test each handler, even if the logic was same ).

