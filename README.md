

## 👤 UserController (`/api/user`)

### 1. 📝 Register User

* **Method**: `POST`
* **URL**: `/api/user/register`
* **Description**: Registers a new user and returns a JWT token.

#### 🧾 Request Body:

```json
{
  "userName": "john_doe",
  "email": "john@example.com",
  "password": "StrongPassword123!"
}
```

#### ✅ Response (200 OK):

```json
{
  "userName": "john_doe",
  "email": "john@example.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

#### ❌ Errors:

* `400 Bad Request`: Email already used or invalid fields.
* `500 Internal Server Error`: Registration process failed.

---

### 2. 🔐 Login User

* **Method**: `POST`
* **URL**: `/api/user/login`
* **Description**: Authenticates a user and returns a JWT token.

#### 🧾 Request Body:

```json
{
  "email": "john@example.com",
  "password": "StrongPassword123!"
}
```

#### ✅ Response (200 OK):

```json
{
  "userName": "john_doe",
  "email": "john@example.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "message": "Login Successful!"
}
```

#### ❌ Error (401 Unauthorized):

```json
"Invalid Email or Password"
```

---

### 3. 🗑️ Delete User by Email

* **Method**: `DELETE`
* **URL**: `/api/user/delete-by-email?email=john@example.com`
* **Description**: Deletes a user by their email address.

#### ✅ Response (200 OK):

```json
{
  "success": true,
  "message": "User deleted successfully.",
  "data": "john@example.com",
  "error": null
}
```

#### ❌ Errors:

* `400 Bad Request`:

```json
{
  "success": false,
  "message": "User with the provided email does not exist.",
  "data": null,
  "error": "UserNotFound"
}
```

* `400 Bad Request` (on delete failure):

```json
{
  "success": false,
  "message": "Failed to delete user.",
  "data": null,
  "error": "DeleteFailed"
}
```



