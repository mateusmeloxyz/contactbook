# ContactBook - Business Requirements

## 1. Overview
**Name:** ContactBook
**Description:** A contact management system for personal and professional use
**Core aspects:** Storing, organizing and managing contact information with validation and security

## 2. Goals
- Provide a simple, intuitive interface for managing contacts
- Ensure data integrity through validation rules
- Support multiple users with authentication (tentative)
- Enable data export for backup purposes (tentative)
- Track changes for audit purposes (tentative)

## 3. User Roles and Permissions

(later)

## 4. Entities

### 4.1 Contact Entity
**Fields:**
- 'First Name' (required, 2-100 chars)
- 'Last Name' (optional, 2-100 chars) (tentative)
- 'Email'  (optional, unique, valid format "email@domain.com")
- 'Phone' (optional, unique, stored as numbers only (mask brazilian format in the front-end))
- 'CreatedAt' (auto-generated)
- 'UpdatedAt (auto-updated on changes)
- 'IsActive' (flag for soft delete, defaults to true)

## 5. Business Logic:

1. **Soft Delete:**
    - Contacts are never trully deleted
    - Deleted contacts are marked as 'IsActive = false'
    - API filters out inactive contacts by default
    - Optional: Admin endpoint to view all contacts (including inactive contacts)

2. **Duplicate prevention:**
    - Prevent duplicate emails
    - Prevent duplicate phone numbers
    - Show helpful error messages with duplicate info

3. **Audit Trail (tentative):**
    - 'CreatedAt' set on creation automatically (can't be modified)
    - 'UpdatedAt' updated on every modification
    - Add versioning for updates in entities (tentative)

## 6. API Endpoints

Using plurals, based on [REST APIs naming convention](https://medium.com/@elmehdi.kzadri_97626/rest-api-naming-conventions-a-guide-to-clear-and-consistent-endpoints-5243423b9d75)

GET /api/contacts - List all active contacts
GET /api/contacts/{id} - Get specific contact
POST /api/contacts - Create new contact
PUT /api/contacts/{id} - Update existing contact
DELETE /api/contacts/{id} - Soft delete contact (set IsActive = false)
GET /api/contacts/inactive - List inactive contacts (tentative admin feature)
PUT /api/contacts/{id}/restore - Restore soft-deleted contact (tentative admin feature)

## 7. Future enhancements (tentative):

- User authentication and authorization
- Contact categories/Group
- Import/export functionality
- Search and advanced filtering
- API rate limiting
- Contact photo upload
- Birthday tracking
- Notes field for contacts