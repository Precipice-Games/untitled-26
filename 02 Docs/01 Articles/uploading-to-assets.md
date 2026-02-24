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

The [01 Assets](../../01%20Assets/) folder is a directory located in the root (the homepage) of the repository. It exists as a central point where team members can share files with each other.

<img src="../00 Assets/uploading-to-assets/01-assets-folder-location.png" width="700"></img>

</div>

<p align="right">(<a href="#article-top">back to top</a>)</p>

<br></br>

<!-- GITHUB VS UNITY -->
<div id="github-vs-unity" style="font-size:16px;">

<h2>GitHub vs. Unity</h2>

It's important to note that [01 Assets](../../01%20Assets/) is different from the default Assets folder in Unity.

The default folder is located in [00 Unity Proj](../../00%20Unity%20Proj/). Its purpose is to act like a toolbox for the project itself.

01 Assets is a separate toolbox folder located outside of the project that is intended to prevent <a href="#merge-conflicts">merge conflicts</a>.

</div>

<p align="right">(<a href="#article-top">back to top</a>)</p>

<br></br>

<!-- MERGE CONFLICTS -->
<div id="merge-conflicts" style="font-size:16px;">

<h2>Merge Conflicts</h2>

Merge conflicts are inconsistencies in files and data that keep you from syncing your repository together. This can become an issue when I pull data from the repository and it's injected into my project, causing errors in the game.

<div style="font-size:16px;">
<details>
  <summary>Merge Conflict Example</summary>

On your computer, you can't have two images in the same location with identical names, right? Your computer will rename the files as follows:

<ol>
  <li>sunset.jpg</li>
  <li>sunset - Copy.jpg</li>
</ol>

Or even:

<ol>
  <li>sunset.jpg</li>
  <li>sunset (1).jpg</li>
</ol>

While your computer understands this, GitHub does not. It will get confused as to which should be named sunset.jpeg. The same thing happens on a large scale with files in our Unity project.

It's important that we contribute to the repository in such a way as to avoid conflicts.
</details>
</div>
</div>

<p align="right">(<a href="#article-top">back to top</a>)</p>

<br></br>

<!-- HOW TO UPLOAD -->
<div id="uploading-to-01-assets" style="font-size:16px;">

<h2>Uploading to 01 Assets</h2>

Please follow the below steps to ensure the assets are shared correctly.

<h3>1. Correct Branch</h3>

The first step to uploading is making sure you are on the right branch. The default branch of the webpage is `main`.

<img src="../00 Assets/uploading-to-assets/branch-on-main.png" width="500"></img>

Navigate to the `assets/01-assets` branch by clicking on the dropdown menu and selecting it.

<img src="../00 Assets/uploading-to-assets/switch-to-assets-branch.png" width="500"></img>

If it is not available, select "view all branches" and click on the correct one.

Your page should now look like this:

<img src="../00 Assets/uploading-to-assets/correct-branch.png" width="500"></img>

<h3>2. Navigate to 01 Assets</h3>

Up next, navigate to the [01 Assets](../../01%20Assets/) folder.

As mentioned earlier, you should be nowhere near [00 Unity Proj](../../00%20Unity%20Proj/)!

<img src="../00 Assets/uploading-to-assets/01-assets-folder-location.png" width="500"></img>

Now that you're in the correct directory, you can start uploading. Make sure to navigate to a sub-folder that fits the relevant needs of your assets.

<img src="../00 Assets/uploading-to-assets/01-assets-sub-folders.png" width="500"></img>



<h3>3. Uploading</h3>

To upload, click the "Add File" button at the top right of your page and choose "Upload Files."

<img src="../00 Assets/uploading-to-assets/upload-files-button.png" width="500"></img>

<p>&nbsp;</p>

<h3>4. Commit Messages</h3>

After you upload, you will see a window down below titled "Commit changes."

<img src="../00 Assets/uploading-to-assets/empty-commit-box.png" width="700"></img>

This is where you can make note of what you're uploading and why. You MUST give your work a title. DO NOT title it "Add files via upload."

Add any relevant bullets that would be helpful for developers.

<img src="../00 Assets/uploading-to-assets/commit-message-example.png" width="700"></img>

Try to let the engineers know when you've uploaded an asset.

<img src="../00 Assets/uploading-to-assets/discord-message.png" width="500"></img>

And that's it, excellent work!

<img src="../00 Assets/uploading-to-assets/thats-all-folks.gif" width="500"></img>

</div>

<p align="right">(<a href="#article-top">back to top</a>)</p>

<br></br>

<!-- ACKNOWLEDGMENTS -->
## Acknowledgements

* This article was authored by [Nicole-Scalera](https://github.com/Nicole-Scalera).

<p align="right">(<a href="#article-top">back to top</a>)</p>
