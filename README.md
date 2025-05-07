## 📘 Full API Documentation

🔗 **Base URL**: [http://chestcancerdetection.runasp.net/](http://chestcancerdetection.runasp.net/)

---

## 🧑‍⚕️ DoctorController (`/api/doctor`)

### 1. 🔍 Get All Doctors

* **Method**: `GET`
* **URL**: `/api/doctor/GetAllDoctor`
* **Description**: Fetches a list of all registered doctors.

#### ✅ Response (200 OK):

```json
[
  {
    "doctorId": 1,
    "name": "Dr. Sarah Connor",
    "specialization": "Oncology",
    "email": "sarah.connor@example.com",
    "phone": "+1234567890",
    "country": "USA",
    "location": "Los Angeles"
  }
]
```

---

### 2. ➕ Add a Doctor

* **Method**: `POST`
* **URL**: `/api/doctor/AddDoctor`
* **Description**: Adds a new doctor to the system.

#### 🧾 Request Body:

```json
{
  "name": "Dr. John Smith",
  "specialization": "Radiologist",
  "email": "john.smith@example.com",
  "phone": "+19876543210",
  "country": "USA",
  "location": "New York"
}
```

#### ✅ Response (200 OK):

```json
"Doctor successfully added."
```

#### ❌ Errors:

* `400 Bad Request`: Missing fields or invalid data.
* `500 Internal Server Error`: Server-side failure.

---

### 3. 🗑️ Delete a Doctor

* **Method**: `DELETE`
* **URL**: `/api/doctor/DeleteDoctor?id=1`
* **Description**: Deletes a doctor by their ID.

#### ✅ Response (204 No Content)

#### ❌ Error (404 Not Found):

```json
"Doctor not found"
```

---

### 4. ✏️ Update a Doctor

* **Method**: `PUT`
* **URL**: `/api/doctor/UpdateDoctor?id=1`
* **Description**: Updates information of an existing doctor.

#### 🧾 Request Body:

```json
{
  "name": "Dr. Jane Updated",
  "specialization": "Pathologist",
  "email": "jane.updated@example.com",
  "phone": "+1478523690",
  "country": "Canada",
  "location": "Toronto"
}
```

#### ✅ Response (200 OK):

```json
"Doctor successfully updated"
```

#### ❌ Error (404 Not Found):

```json
"Doctor not found."
```

---

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
* **Description**: Deletes a user from the system using their email address.
* **Authorization**: 🔒 Admin access required

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

* `400 Bad Request` – Email not found:

```json
{
  "success": false,
  "message": "User with the provided email does not exist.",
  "data": null,
  "error": "UserNotFound"
}
```

* `400 Bad Request` – Delete failed:

```json
{
  "success": false,
  "message": "Failed to delete user.",
  "data": null,
  "error": "DeleteFailed"
}
```

* `403 Forbidden` – Unauthorized access:

```json
"Access denied. Admin privileges required."
```



