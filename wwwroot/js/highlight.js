document.querySelectorAll('.color-last-words').forEach(word => {
    const words = word.textContent.trim().split(' ');
    if (words.length > 2) {
        const lastTwoWords = words.splice(-2).join(' ');
        word.innerHTML = `${words.join(" ")} <span class="highlighted">${lastTwoWords}</span>`;
    }
})