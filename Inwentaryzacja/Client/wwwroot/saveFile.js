export function saveFile(file, byte)
{
    var blob = new Blob([byte], {type: "application/pdf"});
    var link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    var fileName = file;
    link.download = fileName;
    link.click();
 }
export function showPrompt(message)
{
    return prompt(message);
}