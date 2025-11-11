import { validate } from '/js/formValidation.js';

export function initFormValidation(selector = 'input, select, textarea') {
    function debounce(callback, delay = 300) {
        let timeout;
        return (...args) => {
            clearTimeout(timeout);
            timeout = setTimeout(() => callback(...args), delay);
        };
    }

    document.querySelectorAll(selector).forEach(input => {
        const debouncedValidation = debounce(e => {
            const { id, name, value } = e.target;
            if (!id || !name) return;
            const result = validate(id, value);
            toggleValidationClass(`.span-icon-${id}`, result.isValid, name, result.message);
        }, 300);

        input.addEventListener('input', debouncedValidation);
    })

    function toggleValidationClass(span, isValid, name, message) {
        const element = document.querySelector(span);
        const spanValidation = document.querySelector(`[data-valmsg-for="${name}"]`);
        if (!element || !spanValidation) return;

        spanValidation.classList.toggle('field-validation-error', !isValid);
        spanValidation.textContent = isValid ? '' : message;
        element.classList.toggle('valid', isValid);
    }
}