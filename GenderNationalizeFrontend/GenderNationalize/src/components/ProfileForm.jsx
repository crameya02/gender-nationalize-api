import { useState } from "react";
import styles from "./ProfileForm.module.css";
import ProfileResult from "./ProfileResult";
/**
 * A form that takes a first name and predicts the gender and country of origin.
 * Renders a text input, a button, and a loading spinner.
 * When the button is clicked, fetches the profile from the API and displays the result in a popup.
 */
function ProfileForm() {
    const [name, setName] = useState("");
    const [profile, setProfile] = useState(null);
    const [loading, setLoading] = useState(false);
    const [showPopup, setShowPopup] = useState(false);
    const fetchProfile = async () => {
        try {
            setLoading(true);
            const res = await fetch(`${import.meta.env.VITE_API_URL}/Profile?name=${name}`);
            if (!res.ok) throw new Error(`Fetch failed with status ${res.status}`);
            const data = await res.json();
            setProfile(data);
            setShowPopup(true);
        } catch (err) {
            console.error("Error fetching profile:", err.message);
        } finally {
            setLoading(false);
        }
    };



    return (
        <div className={styles.container}>
            <h1 className={styles.title}>Predict Gender and Country of Origin</h1>
            <p className={styles.description}>Enter a first name and we'll calculate the gender and country of origin.</p>
            <input
                className={styles.input}
                type="text"
                value={name}
                onChange={(e) => setName(e.target.value)}
                placeholder="Enter a name"
            />
            {loading && (
                <div className={styles.overlay}>
                    <div className={styles.spinner}></div>
                </div>
            )}
            <button
                onClick={fetchProfile}
                className={styles.button}
                disabled={loading}
            >
                Get Profile
            </button>
            {/* {
                profile && (
                    <div className={styles.result}>
                        <p><strong>Name:</strong> {profile.name}</p>
                        <p><strong>Gender:</strong> {profile.gender}</p>
                        <p><strong>Nationality:</strong> {Array.isArray(profile.nationality) ? profile.nationality.join(", ") : profile.nationality}</p>
                    </div>
                )


            } */}

            {showPopup && profile && (
                <ProfileResult profile={profile} onClose={() => setShowPopup(false)} />
            )}
        </div >
    );
}

export default ProfileForm;
