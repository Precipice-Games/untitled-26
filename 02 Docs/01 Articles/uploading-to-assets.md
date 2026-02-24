<a id="article-top"></a>

<!-- TITLE -->
<br />
<div align="center">

<h1 align="center">Untitled-26</h1>
<h3 align="center">▸ Uploading to the Assets folder ◂</h3>

  <p align="center">
    A guide for anyone contributing items to the <a href="../../01 Assets/">01 Assets</a> folder.<br />
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<div style="font-size:16px;">
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li><a href="#overview">Overview</a></li>
    <li><a href="#what-is-01-assets">What is the 01 Assets Folder?</a></li>
    <li><a href="#github-vs-unity">GitHub vs. Unity</a></li>
    <li><a href="#merge-conflicts">Merge Conflicts</a></li>
      <ol type="i">
        <li><a href="#merge-conflict-example">Example</a></li>
      </ol>
    <li><a href="#uploading-to-01-assets">Uploading to 01 Assets</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
  </ol>
</details>
</div>

<br></br>


<!-- OVERVIEW -->
<div id="overview" style="font-size:16px;">
<h2>Overview</h2>

<p style="font-size:16px;">Hello everyone! For easy reading, I've broken the guide up into bite-sized sections. If you want to upload immediately, jump straight to <a href="#uploading-to-01-assets">this section</a> of the article.

Please read carefully to ensure you commit to the repository correctly.
</div>

<p align="right">(<a href="#article-top">back to top</a>)</p>

<br></br>

<!-- WHAT IS 01 ASSETS -->
<div id="what-is-01-assets" style="font-size:16px;">

<h2>What Is the 01 Assets Folder?</h2>

The [01 Assets](../../01%20Assets/) folder is a directory located in the [root](https://github.com/Nicole-Scalera/project-raven) (the homepage) of the repository. It exists as a central point where team members can share files with each other.

<img src="../00 Assets/uploading-to-assets/01-assets-folder-location.png" width="500"></img>

</div>

<p align="right">(<a href="#article-top">back to top</a>)</p>

<br></br>

<!-- GITHUB VS UNITY -->
<div id="github-vs-unity" style="font-size:16px;">

<h2>GitHub vs. Unity</h2>

It's important to note that [01 Assets](https://github.com/Nicole-Scalera/project-raven/tree/main/01%20Assets) is different from the default Assets folder in Unity.

The default folder is located in [00 Unity Proj](https://github.com/Nicole-Scalera/project-raven/tree/main/00%20Unity%20Proj). Its purpose is to act like a toolbox for the project itself.

Similarly, 01 Assets operates as a toolbox folder, but it allows separate parties to upload assets without any <a href="#merge-conflicts">merge conflicts</a>.

</div>

<p align="right">(<a href="#article-top">back to top</a>)</p>

<br></br>

<!-- MERGE CONFLICTS -->
<div id="merge-conflicts" style="font-size:16px;">

<h2>Merge Conflicts</h2>

If you've never used GitHub, you may be wondering, what in the world are merge conflicts?!

Essentially, they're inconsistencies in files and data that keep you from syncing your repository together. This can become an issue when I pull data from the repository and it's injected into my project, causing errors in the game.

<p>&nbsp;</p>

<div id="merge-conflict-example" style="font-size:16px;">

<h3>Example</h3>

For instance, on your computer, you can't have two images in the same location with identical names, right? Your computer will rename the files as follows:

<ol>
  <li>sunset.jpg</li>
  <li>sunset - Copy.jpg</li>
</ol>

Or even:

<ol>
  <li>sunset.jpg</li>
  <li>sunset (1).jpg</li>
</ol>

</div>

However, while your computer understands how to deal with these issues, GitHub might not. It has tools to figure out conflicting data on a small scale, but changes that are made directly to the main branch of the repository are typically very hard to undo.

Then again, the beauty of GitHub is that there's a <a href="#merge-conflicts">solution</a> to prevent this!

<p align="right">(<a href="#article-top">back to top</a>)</p>

<br></br>

<!-- HOW TO UPLOAD -->
<div id="uploading-to-01-assets" style="font-size:16px;">

<h2>Uploading to 01 Assets</h2>

Please follow the below steps to ensure the assets are shared correctly.

<p>&nbsp;</p>

<h3>1. Correct Folder</h3>

The first step to uploading is making sure you are on the right folder. As mentioned above, you MUST be on the <a href="https://github.com/Nicole-Scalera/project-raven/tree/main/01%20Assets">01 Assets</a> folder in the ROOT of the repository.

That means you should be nowhere near [00 Unity Proj](https://github.com/Nicole-Scalera/project-raven/tree/main/00%20Unity%20Proj)!

<img src="../00 Assets/uploading-to-assets/01-assets-folder-location.png" width="500"></img>

Now that you're in the correct folder, you can start uploading. Make sure to navigate to a folder that fits the relevant needs of your assets.

<p>&nbsp;</p>

<h3>2. Uploading</h3>

To upload, click the "Add File" button at the top right of your page.

<img src="../00 Assets/uploading-to-assets/add-file-button.png" width="500"></img>

In the drop down, choose "Upload Files"

<img src="../00 Assets/uploading-to-assets/upload-files-button.png" width="500"></img>

There will be a popup where you can upload your files.

<img src="../00 Assets/uploading-to-assets/selecting-files-popup.png" width="500"></img>

<p>&nbsp;</p>

<h3>3. Commit Messages</h3>

After you upload, you will also see a window down below titled "Commit changes."

<img src="../00 Assets/uploading-to-assets/commit-menu-empty.png" width="500"></img>

This is where you can make note of what you're uploading and why. It doesn't need to be too descriptive, just a word or two would help for future reference, like so.

<img src="../00 Assets/uploading-to-assets/commit-menu-with-message.png" width="500"></img>

Because the 01 Assets folder is not directly connected to the 00 Unity Proj folder, you don't need to worry about creating a new branch. Just select the green "Commit changes" and you're good to go!
</div>

<p align="right">(<a href="#article-top">back to top</a>)</p>

<br></br>

<!-- ACKNOWLEDGMENTS -->
## Acknowledgements

* This README was authored by Nicole Scalera ([Nicole-Scalera](https://github.com/Nicole-Scalera)).
* I started this README file thru a template from the popular repository, [Best-README-Template](https://github.com/othneildrew/Best-README-Template), by [othneildrew](https://github.com/othneildrew) on GitHub.
* Many edits have been made to make it different and fit a sufficient guide on uploading.

<p align="right">(<a href="#article-top">back to top</a>)</p>
