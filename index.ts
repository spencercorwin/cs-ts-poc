import execa from 'execa';

(async () => {
  const { stdout } = await execa('dotnet', [
    'run',
    'Spencer', // input argument to HelloWorld program
    '-c',
    'Debug',
    '-p',
    '/Users/spencercorwin/Desktop/cs-ts-poc/HelloWorld/HelloWorld', // change this path
  ]);
  console.log(stdout);
})();
