export const validate = (field, value) => {
    const rules = {
        email: {     
            isValid: /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value.trim()),
            message: 'Please enter a valid email address.'
        },
        name: {
            isValid: value.trim().length > 1,
            message: 'At least 2 characters.'
        },
        phoneNumber: {
            isValid: /^\+?[0-9\s\-()]{7,20}$/.test(value.trim()),
            message: 'Phone number must be 10 digits.'
        },
        message: {
            isValid: value.trim().length > 9,
            message: 'Question must be at least 10 characters long.'
        },
        option: {
            isValid: value.trim().length > 0,
            message: 'Please select an option.'
        }
    }
    return rules[field] || { isValid: false, message: 'Unknown field.' };
}