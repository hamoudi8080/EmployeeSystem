
# Example of the Workflow

## User logs in:

1. User enters their credentials on the login page.
2. Server creates a session after verifying the credentials and generates a session ID (`sessionID=abc123`).
3. Server sends the session ID to the client in a cookie, for example:

```http
Set-Cookie: sessionID=abc123; HttpOnly; Secure
```

4. The client’s browser stores the cookie.

## Subsequent Request:

1. The client makes another request to a protected route (e.g., `/profile`).
2. The browser automatically includes the session ID stored in the cookie with the request:

```http
GET /profile HTTP/1.1
Host: example.com
Cookie: sessionID=abc123
```

3. The server receives the request, reads the `sessionID=abc123` from the cookie.
4. The server checks its session store to find the session associated with `abc123`, retrieves the user’s data, and determines whether to allow access.

## How the server knows it is the same client:

The server identifies the client using the session ID stored in the client’s cookie. As long as the client sends the correct session ID with each request, the server can match it to the user’s session stored server-side. This session contains all the necessary information about the user, such as their authentication status, user ID, and other details. If the session ID is valid and the session is still active, the server considers the request authenticated.

## Why is it stored in a cookie?

Cookies are automatically sent with every request to the same domain by the browser, making them a convenient way to maintain user authentication between multiple requests. Since the cookie containing the session ID is sent with every request, the server can maintain continuity of the user’s session.

- **HttpOnly flag**: Ensures the cookie cannot be accessed by JavaScript (prevents XSS attacks).
- **Secure flag**: Ensures the cookie is only sent over HTTPS, providing security during transmission.

## Comparison with JWT:

- In session-based authentication, the session ID is simply an identifier; the session data is stored on the server.
- In JWT-based authentication, the token itself contains all the user information, and the server doesn’t need to store anything (making it stateless).






