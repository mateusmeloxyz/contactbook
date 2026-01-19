import axiosAPI from "./axios";

export const fetchContacts = () => {
  return axiosAPI.get("/contacts");
};

export const fetchContactById = (id) => {
  return axiosAPI.get(`/contacts/${id}`);
};

export const createContact = (contactData) => {
  return axiosAPI.post("/contacts", contactData);
};

export const updateContact = (id, contactData) => {
  return axiosAPI.put(`/contacts/${id}`, contactData);
};

export const deleteContact = (id) => {
  return axiosAPI.delete(`/contacts/${id}`);
};
