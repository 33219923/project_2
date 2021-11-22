export const determineMimeType = (filename: string) => {
  let extension = filename.substring(filename.lastIndexOf('.')).toLocaleLowerCase();
  let mimeType = '';

  switch (extension) {
    case '.bmp':
      mimeType = 'image/bmp';
      break;
    case '.ico':
      mimeType = 'image/vnd.microsoft.icon';
      break;
    case '.jpg':
    case '.jpeg':
      mimeType = 'image/jpeg';
      break;
    case '.gif':
      mimeType = 'image/gif';
      break;
    case '.png':
      mimeType = 'image/png';
      break;
    case '.pdf':
      mimeType = 'application/pdf';
      break;
  }

  return mimeType;
};

export const downloadFile = (blobData: any, filename: string) => {
  let url = convertBlobToUrl(blobData, filename);

  var link = window.document.createElement('a');
  link.href = url;
  link.download = filename;
  link.target = '_blank';
  link.click();
};

export const convertBlobToUrl = (blobData: any, filename: string) => {
  let mimeType = determineMimeType(filename);

  const binaryString = window.atob(blobData);
  const len = binaryString.length;
  const bytes = new Uint8Array(len);
  for (let i = 0; i < len; ++i) {
    bytes[i] = binaryString.charCodeAt(i);
  }
  const blob = new Blob([bytes], { type: mimeType });
  const fileURL = URL.createObjectURL(blob);
  return fileURL;
};

export const PresetColorType = [
  'pink',
  'red',
  'yellow',
  'orange',
  'cyan',
  'green',
  'blue',
  'purple',
  'geekblue',
  'magenta',
  'volcano',
  'gold',
  'lime',
];
