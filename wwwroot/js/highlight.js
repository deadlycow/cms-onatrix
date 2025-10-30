document.querySelectorAll('.color-last-words').forEach(word => {
    const words = word.textContent.trim().split(' ');
    if (words.length > 2) {
        const lastTwoWords = words.splice(-2).join(' ');
        word.innerHTML = `${words.join(" ")} <span class="highlighted">${lastTwoWords}</span>`;
    }
})

const element = document.querySelector('.color-last');

if (element) {
    const words = element.textContent.trim().split(' ');
    if (words.length > 1) {
        const lastWord = words.pop();
        element.innerHTML = `${words.join(" ")} <span class="highlighted">${lastWord}</span>`;
    }
}