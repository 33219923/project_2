export const determineMimeType = (filename: string) => {
  let extension = filename.substring(filename.lastIndexOf('.')).toLocaleLowerCase();
  console.log('Extension=> ', extension);
  let mimeType = '';

  switch (extension) {
    case '.xlsx':
      mimeType = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
      break;
    case '.xls':
      mimeType = 'application/vnd.ms-excel';
      break;
    case '.doc':
      mimeType = 'application/msword';
      break;
    case '.docx':
      mimeType = 'application/vnd.openxmlformats-officedocument.wordprocessingml.document';
      break;
    case '.jpg':
    case '.jpeg':
      mimeType = 'image/jpeg';
      break;
    case '.msg':
      mimeType = 'application/vnd.ms-outlook';
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
