start chrome http://localhost:44315

docker run -p 44315:80 -e ASPNETCORE_ENVIRONMENT=LOC --name TrippleTApp triple-t-app

pause