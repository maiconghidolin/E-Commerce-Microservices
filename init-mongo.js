db.createUser({
    user: "admin",
    pwd: "Test123!",
    roles: [
        {
            role: "readWrite",
            db: "NotificationsDB",
        },
    ],
});
