

## 📘 Full API Documentation

---
 link : http://chestcancerdetection.runasp.net/
## 🧑‍⚕️ DoctorController (`/api/doctor`)

### 1. 🔍 Get All Doctors

- **Method**: `GET`
    
- **URL**: `/api/doctor/GetAllDoctor`
    
- **Description**: Fetches a list of all registered doctors.
    

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

- **Method**: `POST`
    
- **URL**: `/api/doctor/AddDoctor`
    
- **Description**: Adds a new doctor to the system.
    

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

- `400 Bad Request`: Missing fields or invalid data.
    
- `500 Internal Server Error`: Server-side failure.
    

---

### 3. 🗑️ Delete a Doctor

- **Method**: `DELETE`
    
- **URL**: `/api/doctor/DeleteDoctor?id=1`
    
- **Description**: Deletes a doctor by their ID.
    

#### ✅ Response (204 No Content)

#### ❌ Error (404 Not Found):

```json
"Doctor not found"
```

---

### 4. ✏️ Update a Doctor

- **Method**: `PUT`
    
- **URL**: `/api/doctor/UpdateDoctor?id=1`
    
- **Description**: Updates information of an existing doctor.
    

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

- **Method**: `POST`
    
- **URL**: `/api/user/register`
    
- **Description**: Registers a new user and returns a JWT token.
    

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

- `400 Bad Request`: Email already used or invalid fields.
    
- `500 Internal Server Error`: Registration process failed.
    

---

### 2. 🔐 Login User

- **Method**: `POST`
    
- **URL**: `/api/user/login`
    
- **Description**: Authenticates a user and returns a JWT token.
    

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
