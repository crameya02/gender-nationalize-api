import styles from "./ProfileResult.module.css";

/**
 * ProfileResult component
 *
 * @param {Object} props
 * @prop {Object} profile - the profile object from the API response
 * @prop {function} onClose - the function to call when the popup should be closed
 *
 * @returns {ReactElement} the rendered component
 */
function ProfileResult({ profile, onClose }) {
  if (!profile) return null;

  return (
    <div className={styles.overlay}>
      <div className={styles.popup}>
        <button className={styles.closeButton} onClick={onClose}>×</button>
        <h2 className={styles.name}>{profile.name}</h2>
        <div className={styles.content}>
          <div className={styles.genderSection}>
            <p className={styles.sectionTitle}>Gender</p>
            <div className={styles.genderIcon}>
              {profile.gender === "male" ? "♂" : profile.gender === "female" ? "♀" : "?"}
            </div>
            <p className={styles.genderLabel}>{profile.gender}</p>
          </div>
          <div className={styles.countrySection}>
            <p className={styles.sectionTitle}>Country of Origin</p>
 

            <ul className={styles.countryList}>
              {Array.isArray(profile.nationality)
                ? profile.nationality.map((country, idx) => (
                    <li key={idx} className={styles.countryItem}>{country}</li>
                  ))
                : <li className={styles.countryItem}>{profile.nationality}</li>}
            </ul>
          </div>
        </div>
      </div>
      
    </div>
  );
}

export default ProfileResult;
